using SpendSmart.Common.Primitives.Result;
using SpendSmart.Common.Primitives.ServiceLifetimes;
using SpendSmart.Domain.Errors;
using SpendSmart.Domain.Modules.Users.Contracts;
using System.Threading;
using System.Threading.Tasks;

namespace SpendSmart.Domain.Modules.Users;

/// <summary>
/// Represents the user factory.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="UserFactory"/> class.
/// </remarks>
/// <param name="userRepository">The user repository.</param>
/// <param name="passwordHasher">The password hasher.</param>
/// <param name="roleProvider">The role provider.</param>
internal sealed class UserFactory(
                   IUserRepository userRepository,
                   IPasswordHasher passwordHasher,
                   IRoleProvider roleProvider) : IUserFactory, IScoped
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IPasswordHasher _passwordHasher = passwordHasher;
    private readonly IRoleProvider _roleProvider = roleProvider;

    /// <inheritdoc />
    public async Task<Result<User>> CreateAsync(CreateUserRequest createUserRequest,
                                                CancellationToken cancellationToken)
    {
        Result<FirstName> firstNameResult = FirstName.Create(createUserRequest.FirstName);
        Result<LastName> lastNameResult = LastName.Create(createUserRequest.LastName);
        Result<Email> emailResult = Email.Create(createUserRequest.Email);
        Result<Password> passwordResult = Password.Create(createUserRequest.Password);

        var result = Result.FirstFailureOrSuccess(firstNameResult,
                                                  lastNameResult,
                                                  emailResult,
                                                  passwordResult);

        if (result.IsFailure)
        {
            return Result.Failure<User>(result.Error);
        }

        bool emailAlreadyInUse = await _userRepository.AnyWithEmailAsync(emailResult.Value, cancellationToken);

        if (emailAlreadyInUse)
        {
            return Result.Failure<User>(DomainErrors.User.EmailAlreadyInUse);
        }

        var user = User.Create(firstNameResult.Value,
                               lastNameResult.Value,
                               emailResult.Value,
                               passwordResult.Value,
                               _passwordHasher);

        foreach (string role in _roleProvider.GetStandardRoles())
        {
            user.AddRole(role);
        }

        return user;
    }
}
