$solution = "../../../PocMediatR.API"
$project = "../../../PocMediatR.Infra/PocMediatR.Infra.csproj"
$context = "PocMediatRContext"
$migrationName = "InitialCreate"

dotnet ef migrations add $migrationName -s $solution -p $project -c $context -o migartions -v