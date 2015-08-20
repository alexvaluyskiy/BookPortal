using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookPortal.Core.Framework.Domain;
using BookPortal.Core.Framework.Models;
using BookPortal.Web.Domain.Models;
using BookPortal.Web.Domain.Models.Types;
using BookPortal.Web.Models.Responses;
using BookPortal.Web.Repositories;

namespace BookPortal.Web.Services
{
    public class WorksService
    {
        private readonly WorksRepository _worksRepository;
        private readonly MarksRepository _marksRepository;
        private readonly WorkTypesRepository _workTypesRepository;
        private readonly PersonsRepository _personsRepository;

        public WorksService(
            WorksRepository worksRepository,
            MarksRepository marksRepository,
            WorkTypesRepository workTypesRepository,
            PersonsRepository personsRepository)
        {
            _worksRepository = worksRepository;
            _marksRepository = marksRepository;
            _workTypesRepository = workTypesRepository;
            _personsRepository = personsRepository;
        }

        public async Task<ApiObject<WorkResponse>> GetWorksAsync(int personId, int userId)
        {
            // get work types
            var worktypes = await _workTypesRepository.GetWorkTypesDictionaryAsync();

            // get all work links and workIds
            var workLinks = await _worksRepository.BuildWorksTreeAsync(personId);
            var workIds = workLinks.Select(c => c.WorkId).Distinct().ToList();

            // marks
            var marks = await _marksRepository.GetWorkMarkAsync(workIds.ToArray());
            var workMarks = marks.ToDictionary(c => c.WorkId, c => c);
            if (marks.Count > 0 && userId > 0)
            {
                var userMarks = await _marksRepository.GetUserMarkAsync(userId, workIds.ToArray());
                foreach (var userMark in userMarks)
                {
                    MarkResponse workMarkTemp;
                    workMarks.TryGetValue(userMark.WorkId, out workMarkTemp);
                    if (workMarkTemp != null)
                    {
                        workMarks[userMark.WorkId].UserMark = userMark.UserMark;
                    }
                }
            }

            // get all works
            var worksRaw = await _worksRepository.GetWorksByIdsAsync(workIds);

            // get all work's people
            var peopleDic = await _personsRepository.GetPersonsByIdsAsync(workIds);

            List<WorkResponse> works = new List<WorkResponse>();
            foreach (var work in worksRaw)
            {
                var workType = worktypes[work.WorkTypeId];

                // manual restriction to show the work in biblio
                if (work.ShowInBiblio == 2) continue;

                // restrict to show magazines
                if (work.WorkTypeId == 26) continue;

                // restrict children works (non-active)
                var workLink = workLinks.SingleOrDefault(c => c.WorkId == work.Id && c.LinkType == 2);
                if (workLink?.ParentWorkId != null) continue;

                // multiauthors cycles
                var workPeople = peopleDic.GetValueOrDefault(work.Id);
                if (workType.IsNode && !workLinks.Any(c => c.WorkId == work.Id && c.LinkType == 2 && c.ParentWorkId != null))
                {
                    if (workPeople == null || workPeople.Count(c => c.PersonId != personId) == workPeople.Count)
                    {
                        workType = worktypes[50];
                    }
                }

                // remove all non-author's works
                if (workType.WorkTypeId != 50 && workPeople?.Count(c => c.PersonId != personId) == workPeople?.Count)
                {
                    continue;
                }

                var workResponse = CreateWorkResponse(work, workType, peopleDic, workLink, personId, workMarks);

                if (work.ShowSubworksInBiblio == 1 || (work.ShowSubworksInBiblio != 2 && workType.IsNode) || work.WorkTypeId == 50)
                {
                    GetSubworks(workLinks, work, workResponse, worksRaw, worktypes, peopleDic, personId, workMarks);
                }

                works.Add(workResponse);
            }

            return new ApiObject<WorkResponse>(works);
        }

        private void GetSubworks(List<WorkLink> workLinks, Work work, WorkResponse workResponse, List<Work> worksRaw, Dictionary<int, WorkTypeResponse> worktypes,
            Dictionary<int, List<PersonResponse>> peopleDic, int personId, Dictionary<int, MarkResponse> marks)
        {
            var childWorks = workLinks.Where(c => c.ParentWorkId == work.Id).ToList();

            if (childWorks.Count > 0)
            {
                workResponse.ChildWorks = new List<WorkResponse>();
                foreach (var child in childWorks)
                {
                    var childWork = worksRaw.SingleOrDefault(c => c.Id == child.WorkId);
                    var childWorkType = worktypes[childWork.WorkTypeId];

                    var childWorkResponse = CreateWorkResponse(childWork, childWorkType, peopleDic, child, personId, marks);

                    GetSubworks(workLinks, childWork, childWorkResponse, worksRaw, worktypes, peopleDic, personId, marks);

                    workResponse.ChildWorks.Add(childWorkResponse);
                }
            }
        }

        private WorkResponse CreateWorkResponse(Work work, WorkTypeResponse workType, Dictionary<int, List<PersonResponse>> peopleDic, WorkLink workLink, int personId, Dictionary<int, MarkResponse> marks)
        {
            var workResponse = new WorkResponse();

            bool isInnactive = workLink.LinkType == 2 && workLink.ParentWorkId != null;

            // don't show work_id on innactive works
            if (!isInnactive)
            {
                workResponse.WorkId = work.Id;
            }

            if (!string.IsNullOrEmpty(work.RusName))
                workResponse.RusName = work.RusName;

            if (!string.IsNullOrEmpty(work.Name))
                workResponse.Name = work.Name;

            if (!string.IsNullOrEmpty(work.AltName))
                workResponse.AltName = work.AltName;

            workResponse.Year = work.Year;
            workResponse.WorkTypeLevel = workType.Level;
            if (work.InPlans)
            {
                workResponse.InPlans = true;
                workResponse.WorkTypeName = workType.NameSingle;
            }

            if (work.NotFinished)
                workResponse.NotFinished = true;
                
            var people = peopleDic.GetValueOrDefault(work.Id);
            var coauthorsPeople = people?.Where(c => c.PersonId != personId).ToList();
            if (coauthorsPeople?.Count > 0)
            {
                workResponse.Persons = coauthorsPeople;

                if (!coauthorsPeople.SequenceEqual(people))
                {
                    workResponse.CoAuthorType = "coauthor";
                }
                else
                {
                    // 17: anthologies
                    workResponse.CoAuthorType = workType.WorkTypeId == 17 ? "editor" : "author";
                }
            }

            workResponse.PublishType = work.PublishType != 1 ? (int?) work.PublishType : null;

            // word links text
            workResponse.GroupIndex = workLink.GroupIndex;

            if (workLink.IsAddition)
                workResponse.IsAddition = workLink.IsAddition;

            if (!string.IsNullOrEmpty(workLink.BonusText))
                workResponse.BonusText = workLink.BonusText;

            var workMark = marks.GetValueOrDefault(work.Id);
            if (workMark != null)
            {
                workResponse.UserMark = workMark.UserMark;
                workResponse.Rating = Math.Round(workMark.Rating, 2);
                workResponse.VotesCount = workMark.MarksCount;
            }

            workResponse.RootCycleWorkId = null;
            workResponse.RootCycleWorkName = null;
            workResponse.RootCycleWorkTypeId = null;

            return workResponse;
        }

        public Task<WorkResponse> GetWorkAsync(int workId)
        {
            return _worksRepository.GetWorkAsync(workId);
        }

        public async Task<MarkResponse> GetWorkMarkAsync(int workId, int userId)
        {
            var workMark = (await _marksRepository.GetWorkMarkAsync(workId)).SingleOrDefault();

            if (workMark != null)
            {
                var userMark = (await _marksRepository.GetUserMarkAsync(userId, workId)).SingleOrDefault();
                workMark.UserMark = userMark?.UserMark;
            }

            return workMark;
        }
    }
}
