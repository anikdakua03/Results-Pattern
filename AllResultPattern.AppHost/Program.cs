var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.AllResultsPattern>("allresultspattern");

builder.Build().Run();
