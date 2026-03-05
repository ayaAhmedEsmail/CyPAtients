

var builder = DistributedApplication.CreateBuilder(args);

var api = builder.AddProject<Projects.CyPatients>("api");

var frontend = builder.AddNpmApp(
        name: "frontend",
        workingDirectory: @"E:\repos\Vue\PatientForm\PatientForm",
        scriptName: "dev"
    )
    .WithReference(api)
    .WithEnvironment("VITE_API_URL", api.GetEndpoint("http"))
    .WithExternalHttpEndpoints();

builder.Build().Run();