var builder = DistributedApplication.CreateBuilder(args);

var api = builder.AddProject<Projects.CyPatients>("api");

var frontend = builder.AddNpmApp(
        name: "frontend",
        workingDirectory: @"E:\repos\Vue\PatientForm\PatientForm",
        scriptName: "dev"
    )
    .WithReference(api)
    .WithEnvironment("VITE_API_URL", api.GetEndpoint("http"))
    .WithEndpoint(port: 5173,scheme:"http", env: "PORT")
    .WithExternalHttpEndpoints();

builder.Build().Run();