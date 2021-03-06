﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using TBSTitlesApp.Models;

namespace TBSTitlesApp.Services
{
    public class TitlesController : ApiController
    {
        private readonly TitlesEntities _titlesRepo;

        public TitlesController()
        {
            _titlesRepo = new TitlesEntities();
        }

        [HttpGet]
        public List<TitleResultModel> GetAllTitles(string title)
        {
            var repo = new TitlesEntities();
            var results = _titlesRepo.Titles.Where(x => x.TitleName.Contains(title)).ToList();

            List<TitleResultModel> trmList = new List<TitleResultModel>();

            foreach (var result in results)
            {
                TitleResultModel trm = new TitleResultModel();
                trm.TitleID = result.TitleId;
                trm.TitleName = result.TitleName;
                trm.ReleaseYear = result.ReleaseYear.ToString();                
                trmList.Add(trm);
            }
            
            return trmList;
        }
        
        [HttpGet]
        public TitleDetailsModel GetTitleDetails(string id)
        {
            var titleID = Convert.ToInt32(id);
            var repo = new TitlesEntities();
            var titles = _titlesRepo.Titles.Where(x => x.TitleId == titleID).FirstOrDefault();   
            
            var awards = titles.Awards.Where(x => x.AwardWon == true).OrderByDescending(x => x.AwardYear).ToList();
            var nominations = titles.Awards.Where(x => x.AwardWon == false).OrderByDescending(x => x.AwardYear).ToList();

            var awardsList = new List<AwardModel>();
            foreach (var award in awards)
            {
                AwardModel am = new AwardModel();
                am.Award = award.Award1;
                am.AwardCompany = award.AwardCompany;
                am.AwardYear = award.AwardYear.ToString();
                awardsList.Add(am);
            }

            var nominationsList = new List<AwardModel>();
            foreach (var nomination in nominations)
            {
                AwardModel am = new AwardModel();
                am.Award = nomination.Award1;
                am.AwardCompany = nomination.AwardCompany;
                am.AwardYear = nomination.AwardYear.ToString();
                nominationsList.Add(am);
            }

            TitleDetailsModel tdm = new TitleDetailsModel();
            tdm.ReleaseYear = titles.ReleaseYear.ToString();
            tdm.TitleName = titles.TitleName;
            tdm.Genres = titles.TitleGenres.Select(x => x.Genre.Name).ToList();
            tdm.Description = titles.StoryLines.FirstOrDefault().Description;
            tdm.Participants = titles.TitleParticipants.Select(x => x.Participant.Name).ToList();
            tdm.Awards = awardsList;
            tdm.Nominations = nominationsList;

            return tdm;
        }

    }
}