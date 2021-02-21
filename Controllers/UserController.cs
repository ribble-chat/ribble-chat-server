using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RibbleChatServer.Data;
using RibbleChatServer.Models;

[ApiController]
public class UserController : ControllerBase
{
    private readonly UserDbContext dbContext;
    private readonly UserManager<User> userManager;
    private readonly SignInManager<User> signInManager;

    public UserController(UserDbContext dbContext, UserManager<User> userManager, SignInManager<User> signInManager, IChatDb db)
    {
        this.dbContext = dbContext;
        this.userManager = userManager;
        this.signInManager = signInManager;
    }

    [HttpGet]
    [Route("/api")]
    public IActionResult RibbleApiRoot()
    {
        return Ok("Welcome to the Ribble API!");
    }

    [HttpPost]
    [Route("/api/users")]
    public async Task<ActionResult<User>> Register([FromBody] RegisterUserInfo userInfo)
    {
        var zxcvbnResult = Zxcvbn.Core.EvaluatePassword(userInfo.Password);
        if (zxcvbnResult.Score < 3) return UnprocessableEntity("Password is too weak");
        var user = new User(
            FirstName: userInfo.FirstName,
            LastName: userInfo.LastName,
            UserName: userInfo.Username,
            Email: userInfo.Email
        );
        var userCreationResult = await userManager.CreateAsync(user, userInfo.Password);
        if (!userCreationResult.Succeeded)
            return UnprocessableEntity(userCreationResult.Errors.First());
        return Created("", (UserResponse)user);
    }

    [HttpPost]
    [Route("/api/auth")]
    public async Task<IActionResult> Login(LoginUserInfo loginInfo)
    {
        var user = await userManager.FindByEmailAsync(loginInfo.UsernameOrEmail)
            ?? await userManager.FindByNameAsync(loginInfo.UsernameOrEmail);

        if (user is null)
            return NotFound($"User with email or username {loginInfo.UsernameOrEmail} does not exist");

        var loginResult = await signInManager.PasswordSignInAsync(user, loginInfo.Password, false, false);
        if (loginResult.Succeeded)
            return Ok((UserResponse)user);
        else
            return BadRequest("Incorrect Password");
    }
}
