﻿using PubLibIS.DAL.Interfaces;
using PubLibIS.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PubLibIS.DAL.Repositories.EntityFramework
{
    public class PeriodicalEditionRepository: IPeriodicalEditionRepository
    {
        private LibraryEntityFrameworkContext context;

        public PeriodicalEditionRepository(LibraryEntityFrameworkContext context)
        {
            this.context = context;
        }

        public int Create(PeriodicalEdition periodicalEdition)
        {
            context.PeriodicalEditions.Add(periodicalEdition);
            context.SaveChanges();
            return periodicalEdition.Id;
        }

        public void Delete(int periodicalEditionId)
        {
            var periodicalEdition = Read(periodicalEditionId);
            context.PeriodicalEditions.Remove(periodicalEdition);
        }

        public PeriodicalEdition Read(int periodicalEditionId)
        {
            return context.PeriodicalEditions.Find(periodicalEditionId);
        }

        public IEnumerable<PeriodicalEdition> Read()
        {
            return context.PeriodicalEditions.AsEnumerable();
        }

        public IEnumerable<PeriodicalEdition> Read(int skip, int take)
        {
            return context.PeriodicalEditions.Skip(skip).Take(take).AsEnumerable();
        }
        public IEnumerable<PeriodicalEdition> ReadByPeriodicalId(int periodicalId)
        {
            return context.PeriodicalEditions.Where(x => x.Periodical.Id == periodicalId).AsEnumerable();
        }

        public void Update(PeriodicalEdition periodicalEdition)
        {
            var current = Read(periodicalEdition.Id);
            context.Entry(current).CurrentValues.SetValues(periodicalEdition);
        }

        public IEnumerable<PeriodicalEdition> Where(Func<PeriodicalEdition, bool> predicate)
        {
            return context.PeriodicalEditions.Where(predicate).AsEnumerable();
        }
    }
}