﻿using CleanArchitecture.Application.Interfaces.Infrastructure;
using CleanArchitecture.Application.Interfaces.Persistence;
using CleanArchitecture.Common.Dates;
using CleanArchitecture.Persistance.Shared;
using StructureMap;
using StructureMap.Graph;

namespace CleanArchitecture.Specification.Shared
{
    public static class IoC
    {
        public static IContainer Initialize(AppContext appContext)
        {
            ObjectFactory.Initialize(x =>
            {
                SetScanningPolicy(x);

                x.For<IDatabaseContext>()
                    .Use(appContext.DatabaseContext);

                x.For<IInventoryService>()
                    .Use(appContext.InventoryService);

                x.For<IDateService>()
                    .Use(appContext.DateService);

            });

            return ObjectFactory.Container;
        }

        private static void SetScanningPolicy(IInitializationExpression x)
        {
            x.Scan(scan =>
            {
                scan.AssembliesFromApplicationBaseDirectory(
                    filter => filter.FullName.StartsWith("CleanArchitecture"));

                scan.WithDefaultConventions();
            });
        }
    }
}