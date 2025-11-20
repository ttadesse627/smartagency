# SmartAgency

SmartAgency is an ASP.NET Core microservice-style API for managing applicants, processes, tickets, partners and related resources. The solution is structured into layered projects (API, Application, Domain, Infrastructure, Utility) and includes authentication (JWT), persistence and unit tests for application logic.

## Key features

- REST API endpoints for applicants, orders, tickets, resources and more (see `src/AppDiv.SmartAgency.API/Controllers`).
- JWT-based authentication and token validation (see `appsettings.json` and `JwtSettings`).
- Clean layered architecture: `Application`, `Domain`, `Infrastructure`, `API`, and `Utility` projects.
- Unit tests for application services located under `tests/AppDiv.SmartAgency.Application.UnitTests`.

## When this is useful

Use this project as a starting point for building an agency/CRM-like backend that requires: authentication, role/permission checks, process tracking and extensible domain logic separated from transport and persistence concerns.

## Getting started (developer)

Prerequisites

- .NET SDK 10.0 (or the SDK version used by the solution)
- A database engine compatible with the project's EF Core setup (check `src/AppDiv.SmartAgency.Infrastructure/Context` and `Migrations`).

Clone the repository

```batch
git clone https://github.com/ttadesse627/smartagency.git
cd smartagency
```

Build the solution

```batch
dotnet build SmartAgency.sln
```

Run the API locally

```batch
dotnet run --project src/AppDiv.SmartAgency.API/AppDiv.SmartAgency.API.csproj
```

Run the unit tests

```batch
dotnet test tests/AppDiv.SmartAgency.Application.UnitTests/AppDiv.SmartAgency.Application.UnitTests.csproj
```

Configuration

- Application configuration lives in `src/AppDiv.SmartAgency.API/appsettings.json` (and `appsettings.Development.json`).
- The API uses a `JwtSettings` section for token configuration — ensure values for `Issuer`, `Audience`, `SecretKey` and `ExpiryMinutes` are set.
- Connection strings and environment-specific secrets should be supplied using environment variables or `appsettings.Development.json` during development.

Quick API test (example)

1. Start the API.
2. Obtain a JWT token via the authentication endpoint (see `AuthController`).
3. Call a secured endpoint, for example:

```bash
curl -H "Authorization: Bearer <token>" https://localhost:5001/api/Applicant
```

Replace port and route as needed (check the server output when the app starts).

## Project layout (important files)

- `src/AppDiv.SmartAgency.API/` — Web API project and controllers.
- `src/AppDiv.SmartAgency.Application/` — Application services, DTOs, mapping and features.
- `src/AppDiv.SmartAgency.Domain/` — Domain entities, enums and domain utilities.
- `src/AppDiv.SmartAgency.Infrastructure/` — Persistence, EF Core `DbContext`, migrations, and external integrations.
- `src/AppDiv.SmartAgency.Utility/` — Shared helpers and utilities.
- `tests/` — Unit test projects.

## Extending and contributing

- Follow standard GitHub flow: branch from `master`, open a PR back to `master`.
- For contribution guidelines, add or consult `docs/CONTRIBUTING.md` or `.github/CONTRIBUTING.md` (create if needed).
- File an issue if you find bugs or have feature requests.

## Help and support

- For repo-specific questions, open an issue in this repository.
- For urgent local setup problems, inspect logs produced by the API and ensure your `appsettings.*.json` and environment variables are set correctly.

## License

See the `LICENSE` file in the repository root for licensing details.

## Maintainers

Maintained by the project owner and contributors. See the repository `Insights` / `Contributors` on GitHub for details.

---

If you'd like, I can also add a small `docs/` folder with CONTRIBUTING and a quick architecture diagram. Want me to scaffold that next?





# Smart Agency

## Getting started

To make it easy for you to get started with GitLab, here's a list of recommended next steps.

Already a pro? Just edit this README.md and make it your own. Want to make it easy? [Use the template at the bottom](#editing-this-readme)!

## Add your files

- [ ] [Create](https://docs.gitlab.com/ee/user/project/repository/web_editor.html#create-a-file) or [upload](https://docs.gitlab.com/ee/user/project/repository/web_editor.html#upload-a-file) files
- [ ] [Add files using the command line](https://docs.gitlab.com/ee/gitlab-basics/add-file.html#add-a-file-using-the-command-line) or push an existing Git repository with the following command:

```
cd existing_repo
git remote add origin https://gitlab.com/barich33/SmartAgency.git
git branch -M main
git push -uf origin main
```

## Integrate with your tools

- [ ] [Set up project integrations](https://gitlab.com/barich33/SmartAgency/-/settings/integrations)

## Collaborate with your team

- [ ] [Invite team members and collaborators](https://docs.gitlab.com/ee/user/project/members/)
- [ ] [Create a new merge request](https://docs.gitlab.com/ee/user/project/merge_requests/creating_merge_requests.html)
- [ ] [Automatically close issues from merge requests](https://docs.gitlab.com/ee/user/project/issues/managing_issues.html#closing-issues-automatically)
- [ ] [Enable merge request approvals](https://docs.gitlab.com/ee/user/project/merge_requests/approvals/)
- [ ] [Automatically merge when pipeline succeeds](https://docs.gitlab.com/ee/user/project/merge_requests/merge_when_pipeline_succeeds.html)

## Test and Deploy

Use the built-in continuous integration in GitLab.

- [ ] [Get started with GitLab CI/CD](https://docs.gitlab.com/ee/ci/quick_start/index.html)
- [ ] [Analyze your code for known vulnerabilities with Static Application Security Testing(SAST)](https://docs.gitlab.com/ee/user/application_security/sast/)
- [ ] [Deploy to Kubernetes, Amazon EC2, or Amazon ECS using Auto Deploy](https://docs.gitlab.com/ee/topics/autodevops/requirements.html)
- [ ] [Use pull-based deployments for improved Kubernetes management](https://docs.gitlab.com/ee/user/clusters/agent/)
- [ ] [Set up protected environments](https://docs.gitlab.com/ee/ci/environments/protected_environments.html)

## Smart Agency

## Description

Smart Agecy is the project that used as management tool for an agency that accepts people who interested in working abroad and connects with employers of abroad.

## Badges

On some READMEs, you may see small images that convey metadata, such as whether or not all the tests are passing for the project. You can use Shields to add some to your README. Many services also have instructions for adding a badge.

## Visuals

Depending on what you are making, it can be a good idea to include screenshots or even a video (you'll frequently see GIFs rather than actual videos). Tools like ttygif can help, but check out Asciinema for a more sophisticated method.

## Installation

Within a particular ecosystem, there may be a common way of installing things, such as using Yarn, NuGet, or Homebrew. However, consider the possibility that whoever is reading your README is a novice and would like more guidance. Listing specific steps helps remove ambiguity and gets people to using your project as quickly as possible. If it only runs in a specific context like a particular programming language version or operating system or has dependencies that have to be installed manually, also add a Requirements subsection.

## Usage

Use examples liberally, and show the expected output if you can. It's helpful to have inline the smallest example of usage that you can demonstrate, while providing links to more sophisticated examples if they are too long to reasonably include in the README.

## Support

Tell people where they can go to for help. It can be any combination of an issue tracker, a chat room, an email address, etc.

## Roadmap

If you have ideas for releases in the future, it is a good idea to list them in the README.

## Contributing

State if you are open to contributions and what your requirements are for accepting them.

For people who want to make changes to your project, it's helpful to have some documentation on how to get started. Perhaps there is a script that they should run or some environment variables that they need to set. Make these steps explicit. These instructions could also be useful to your future self.

You can also document commands to lint the code or run tests. These steps help to ensure high code quality and reduce the likelihood that the changes inadvertently break something. Having instructions for running tests is especially helpful if it requires external setup, such as starting a Selenium server for testing in a browser.

## Authors and acknowledgment

Show your appreciation to those who have contributed to the project.

## License

For open source projects, say how it is licensed.

## Project status

If you have run out of energy or time for your project, put a note at the top of the README saying that development has slowed down or stopped completely. Someone may choose to fork your project or volunteer to step in as a maintainer or owner, allowing your project to keep going. You can also make an explicit request for maintainers.
