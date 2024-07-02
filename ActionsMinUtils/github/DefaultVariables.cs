namespace ActionsMinUtils.github;

/// <summary>
///     Convenient constants for default environment variables as documented in
///     https://docs.github.com/en/actions/learn-github-actions/variables#default-environment-variables.
/// </summary>
public class DefaultVariables
{
    /// <summary>
    ///     Always set to <c>true</c>.
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public static readonly string CI = "CI";

    /// <summary>
    ///     The name of the action currently running, or the
    ///     <a href="https://docs.github.com/en/actions/using-workflows/workflow-syntax-for-github-actions#jobsjob_idstepsid">id</a>
    ///     of a step. For example, for an action,
    ///     <c>__repo-owner_name-of-action-repo</c>.
    ///     <p />
    ///     GitHub removes special characters, and uses the name <c>__run</c> when the current step runs a script without an
    ///     <c>id</c>. If
    ///     you use the same script or action more than once in the same job, the name will include a suffix that consists of
    ///     the sequence number preceded by an underscore. For example, the first script you run will have the name
    ///     <c>__run</c>, and
    ///     the second script will be named <c>__run_2</c>. Similarly, the second invocation of <c>actions/checkout</c> will be
    ///     <c>actionscheckout2</c>.
    /// </summary>
    public static readonly string GitHubAction = "GITHUB_ACTION";

    /// <summary>
    ///     The path where an action is located. This property is only supported in composite actions. You can use this path to
    ///     change directories to where the action is located and access other files in that same repository. For example,
    ///     <c>/home/runner/work/_actions/repo-owner/name-of-action-repo/v1</c>.
    /// </summary>
    public static readonly string GitHubActionPath = "GITHUB_ACTION_PATH";

    /// <summary>
    ///     For a step executing an action, this is the owner and repository name of the action. For example,
    ///     <c>actions/checkout</c>.
    /// </summary>
    public static readonly string GitHubActionRepository = "GITHUB_ACTION_REPOSITORY";

    /// <summary>
    ///     Always set to <c>true</c> when GitHub Actions is running the workflow. You can use this variable to differentiate
    ///     when tests are being run locally or by GitHub Actions.
    /// </summary>
    public static readonly string GitHubActions = "GITHUB_ACTIONS";

    /// <summary>
    ///     The name of the person or app that initiated the workflow. For example, <c>octocat</c>.
    /// </summary>
    public static readonly string GitHubActor = "GITHUB_ACTOR";

    /// <summary>
    ///     The account ID of the person or app that triggered the initial workflow run. For example, <c>1234567</c>. Note that
    ///     this is different from the actor username.
    /// </summary>
    public static readonly string GitHubActorId = "GITHUB_ACTOR_ID";

    /// <summary>
    ///     Returns the API URL. For example: <c>https://api.github.com</c>.
    /// </summary>
    public static readonly string GitHubApiUrl = "GITHUB_API_URL";

    /// <summary>
    ///     The name of the base ref or target branch of the pull request in a workflow run. This is only set when the event
    ///     that triggers a workflow run is either <c>pull_request</c> or <c>pull_request_target</c>. For example, <c>main</c>.
    /// </summary>
    public static readonly string GitHubBaseRef = "GITHUB_BASE_REF";

    /// <summary>
    ///     The path on the runner to the file that sets variables from workflow commands. This file is unique to the current
    ///     step and changes for each step in a job. For example,
    ///     <c>/home/runner/work/_temp/_runner_file_commands/set_env_87406d6e-4979-4d42-98e1-3dab1f48b13a</c>. For more
    ///     information, see
    ///     <a
    ///         href="https://docs.github.com/en/actions/using-workflows/workflow-commands-for-github-actions#setting-an-environment-variable">
    ///         "Workflow
    ///         commands for GitHub Actions.
    ///     </a>
    ///     "
    /// </summary>
    public static readonly string GitHubEnv = "GITHUB_ENV";

    /// <summary>
    ///     The name of the event that triggered the workflow. For example, <c>workflow_dispatch</c>.
    /// </summary>
    public static readonly string GitHubEventName = "GITHUB_EVENT_NAME";

    /// <summary>
    ///     The path to the file on the runner that contains the full event webhook payload. For example,
    ///     <c>/github/workflow/event.json</c>.
    /// </summary>
    public static readonly string GitHubEventPath = "GITHUB_EVENT_PATH";

    /// <summary>
    ///     Returns the GraphQL API URL. For example: <c>https://api.github.com/graphql</c>.
    /// </summary>
    public static readonly string GitHubGraphqlUrl = "GITHUB_GRAPHQL_URL";

    /// <summary>
    ///     The head ref or source branch of the pull request in a workflow run. This property is only set when the event that
    ///     triggers a workflow run is either <c>pull_request</c> or <c>pull_request_target</c>. For example,
    ///     <c>feature-branch-1</c>.
    /// </summary>
    public static readonly string GitHubHeadRef = "GITHUB_HEAD_REF";

    /// <summary>
    ///     The
    ///     <a href="https://docs.github.com/en/actions/using-workflows/workflow-syntax-for-github-actions#jobsjob_id">job_id</a>
    ///     of the current job. For example, <c>greeting_job</c>.
    /// </summary>
    public static readonly string GitHubJob = "GITHUB_JOB";

    /// <summary>
    ///     The path on the runner to the file that sets the current step's outputs from workflow commands. This file is unique
    ///     to the current step and changes for each step in a job. For example,
    ///     <c>/home/runner/work/_temp/_runner_file_commands/set_output_a50ef383-b063-46d9-9157-57953fc9f3f0</c>. For more
    ///     information, see
    ///     <a
    ///         href="https://docs.github.com/en/actions/using-workflows/workflow-commands-for-github-actions#setting-an-output-parameter">
    ///         "Workflow
    ///         commands for GitHub Actions."
    ///     </a>
    /// </summary>
    public static readonly string GitHubOutput = "GITHUB_OUTPUT";

    /// <summary>
    ///     The path on the runner to the file that sets system PATH variables from workflow commands. This file is unique to
    ///     the current step and changes for each step in a job. For example,
    ///     <c>/home/runner/work/_temp/_runner_file_commands/add_path_899b9445-ad4a-400c-aa89-249f18632cf5</c>. For more
    ///     information, see
    ///     <a
    ///         href="https://docs.github.com/en/actions/using-workflows/workflow-commands-for-github-actions#adding-a-system-path">
    ///         "Workflow
    ///         commands for GitHub Actions."
    ///     </a>
    /// </summary>
    public static readonly string GitHubPath = "GITHUB_PATH";

    /// <summary>
    ///     The fully-formed ref of the branch or tag that triggered the workflow run. For workflows triggered by push, this is
    ///     the branch or tag ref that was pushed. For workflows triggered by pull_request, this is the pull request merge
    ///     branch. For workflows triggered by release, this is the release tag created. For other triggers, this is the branch
    ///     or tag ref that triggered the workflow run. This is only set if a branch or tag is available for the event type.
    ///     The ref given is fully-formed, meaning that for branches the format is refs/heads/
    ///     &lt;branch_name>, for pull requests it is refs/pull/&lt;pr_number>/merge, and for tags it is refs/tags/&lt;
    ///     tag_name>. For example, refs/heads/feature-branch-1.
    /// </summary>
    public static readonly string GitHubRef = "GITHUB_REF";

    /// <summary>
    ///     The short ref name of the branch or tag that triggered the workflow run. This value matches the branch or tag name
    ///     shown on GitHub. For example, <c>feature-branch-1</c>.
    ///     <p />
    ///     For pull requests, the format is <c>&lt;pr_number&gt;/merge</c>.
    /// </summary>
    public static readonly string GitHubRefName = "GITHUB_REF_NAME";

    /// <summary>
    ///     <c>true</c> if branch protections or
    ///     <a
    ///         href="https://docs.github.com/en/repositories/configuring-branches-and-merges-in-your-repository/managing-rulesets/managing-rulesets-for-a-repository">
    ///         rulesets
    ///     </a>
    ///     are configured for the ref that triggered the workflow run.
    /// </summary>
    public static readonly string GitHubRefProtected = "GITHUB_REF_PROTECTED";

    /// <summary>
    ///     The type of ref that triggered the workflow run. Valid values are <c>branch</c> or <c>tag</c>.
    /// </summary>
    public static readonly string GitHubRefType = "GITHUB_REF_TYPE";

    /// <summary>
    ///     The owner and repository name. For example, <c>octocat/Hello-World</c>.
    /// </summary>
    public static readonly string GitHubRepository = "GITHUB_REPOSITORY";

    /// <summary>
    ///     The ID of the repository. For example, <c>123456789</c>. Note that this is different from the repository name.
    /// </summary>
    public static readonly string GitHubRepositoryId = "GITHUB_REPOSITORY_ID";

    /// <summary>
    ///     The repository owner's name. For example, <c>octocat</c>.
    /// </summary>
    public static readonly string GitHubRespositoryOwner = "GITHUB_REPOSITORY_OWNER";

    /// <summary>
    ///     The repository owner's account ID. For example, <c>1234567</c>. Note that this is different from the owner's name.
    /// </summary>
    public static readonly string GitHubRepositoryOwnerId = "GITHUB_REPOSITORY_OWNER_ID";

    /// <summary>
    ///     The number of days that workflow run logs and artifacts are kept. For example, <c>90</c>.
    /// </summary>
    public static readonly string GitHubRetentionDays = "GITHUB_RETENTION_DAYS";

    /// <summary>
    ///     A unique number for each attempt of a particular workflow run in a repository. This number begins at 1 for the
    ///     workflow run's first attempt, and increments with each re-run. For example, <c>3</c>.
    /// </summary>
    public static readonly string GitHubRunAttempt = "GITHUB_RUN_ATTEMPT";

    /// <summary>
    ///     A unique number for each workflow run within a repository. This number does not change if you re-run the workflow
    ///     run. For example, <c>1658821493</c>.
    /// </summary>
    public static readonly string GitHubRunId = "GITHUB_RUN_ID";

    /// <summary>
    ///     A unique number for each run of a particular workflow in a repository. This number begins at 1 for the workflow's
    ///     first run, and increments with each new run. This number does not change if you re-run the workflow run. For
    ///     example, <c>3</c>.
    /// </summary>
    public static readonly string GitHubRunNumber = "GITHUB_RUN_NUMBER";

    /// <summary>
    ///     The URL of the GitHub server. For example: <c>https://github.com</c>.
    /// </summary>
    public static readonly string GitHubServerUrl = "GITHUB_SERVER_URL";

    /// <summary>
    ///     The commit SHA that triggered the workflow. The value of this commit SHA depends on the event that triggered the
    ///     workflow. For more information, see "
    ///     <a href="https://docs.github.com/en/actions/using-workflows/events-that-trigger-workflows">
    ///         Events that trigger
    ///         workflows.
    ///     </a>
    ///     " For example, <c>ffac537e6cbbf934b08745a378932722df287a53</c>.
    /// </summary>
    public static readonly string GitHubSha = "GITHUB_SHA";

    /// <summary>
    ///     The path on the runner to the file that contains job summaries from workflow commands. The path to this file is
    ///     unique to the current step and changes for each step in a job. For example,
    ///     <c>/home/runner/_layout/_work/_temp/_runner_file_commands/step_summary_1cb22d7f-5663-41a8-9ffc-13472605c76c</c>.
    ///     For more information, see "
    ///     <a
    ///         href="https://docs.github.com/en/actions/using-workflows/workflow-commands-for-github-actions#adding-a-job-summary">
    ///         Workflow
    ///         commands for GitHub Actions.
    ///     </a>
    ///     "
    /// </summary>
    public static readonly string GitHubStepSummary = "GITHUB_STEP_SUMMARY";

    /// <summary>
    ///     The username of the user that initiated the workflow run. If the workflow run is a re-run, this value may differ
    ///     from <c>github.actor</c>. Any workflow re-runs will use the privileges of <c>github.actor</c>, even if the actor
    ///     initiating the re-run (<c>github.triggering_actor</c>) has different privileges.
    /// </summary>
    public static readonly string GitHubTriggeringActor = "GITHUB_TRIGGERING_ACTOR";

    /// <summary>
    ///     The name of the workflow. For example, <c>My test workflow</c>. If the workflow file doesn't specify a <c>name</c>,
    ///     the value of this variable is the full path of the workflow file in the repository.
    /// </summary>
    public static readonly string GitHubWorkflow = "GITHUB_WORKFLOW";

    /// <summary>
    ///     The ref path to the workflow. For example,
    ///     <c>octocat/hello-world/.github/workflows/my-workflow.yml@refs/heads/my_branch</c>.
    /// </summary>
    public static readonly string GitHubWorkflowRef = "GITHUB_WORKFLOW_REF";

    /// <summary>
    ///     The commit SHA for the workflow file.
    /// </summary>
    public static readonly string GitHubWorkflowSha = "GITHUB_WORKFLOW_SHA";

    /// <summary>
    ///     The default working directory on the runner for steps, and the default location of your repository when using the
    ///     <a href="https://github.com/actions/checkout">
    ///         <c>checkout</c>
    ///     </a>
    ///     action. For example, <c>/home/runner/work/my-repo-name/my-repo-name</c>.
    /// </summary>
    public static readonly string GitHubWorkspace = "GITHUB_WORKSPACE";

    /// <summary>
    ///     The architecture of the runner executing the job. Possible values are <c>X86</c>, <c>X64</c>, <c>ARM</c>, or
    ///     <c>ARM64</c>.
    /// </summary>
    public static readonly string RunnerArch = "RUNNER_ARCH";

    /// <summary>
    ///     This is set only if
    ///     <a href="https://docs.github.com/en/actions/monitoring-and-troubleshooting-workflows/enabling-debug-logging">
    ///         debug
    ///         logging
    ///     </a>
    ///     is enabled, and always has the value of <c>1</c>. It can be useful as an indicator to enable
    ///     additional debugging or verbose logging in your own job steps.
    /// </summary>
    public static readonly string RunnerDebug = "RUNNER_DEBUG";

    /// <summary>
    ///     The name of the runner executing the job. This name may not be unique in a workflow run as runners at the
    ///     repository and organization levels could use the same name. For example, <c>Hosted Agent</c>.
    /// </summary>
    public static readonly string RunnerName = "RUNNER_NAME";

    /// <summary>
    ///     The operating system of the runner executing the job. Possible values are <c>Linux</c>, <c>Windows</c>, or
    ///     <c>macOS</c>. For example, <c>Windows</c>.
    /// </summary>
    public static readonly string RunnerOs = "RUNNER_OS";

    /// <summary>
    ///     The path to a temporary directory on the runner. This directory is emptied at the beginning and end of each job.
    ///     Note that files will not be removed if the runner's user account does not have permission to delete them. For
    ///     example, <c>D:\a\_temp</c>.
    /// </summary>
    public static readonly string RunnerTemp = "RUNNER_TEMP";

    /// <summary>
    ///     The path to the directory containing preinstalled tools for GitHub-hosted runners. For more information, see "
    ///     <a
    ///         href="https://docs.github.com/en/actions/using-github-hosted-runners/about-github-hosted-runners#supported-software">
    ///         Using
    ///         GitHub-hosted runners
    ///     </a>
    ///     ". For example, <c>C:\hostedtoolcache\windows</c>.
    /// </summary>
    public static readonly string RunnerToolCache = "RUNNER_TOOL_CACHE";
}