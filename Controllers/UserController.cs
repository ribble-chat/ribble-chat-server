using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RibbleChatServer.Data;
using RibbleChatServer.Models;

[ApiController]
public class UserController : ControllerBase
{
    private readonly UserDbContext dbContext;
    private readonly UserManager<User> userManager;

    public UserController(UserDbContext dbContext, UserManager<User> userManager, IChatDb db)
    {
        this.dbContext = dbContext;
        this.userManager = userManager;
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
        var user = new User(
            FirstName: userInfo.FirstName,
            LastName: userInfo.LastName,
            UserName: userInfo.Username,
            Email: userInfo.Email
        );
        var userCreationResult = await userManager.CreateAsync(user, userInfo.Password);
        if (!userCreationResult.Succeeded)
            return Problem(userCreationResult.Errors.First().Description, null, 422);
        return Created("", user);
    }

    [HttpPost]
    [Route("/api/auth")]
    public async Task<IActionResult> Login(LoginUserInfo loginInfo)
    {
        var user = await userManager.FindByEmailAsync(loginInfo.UsernameOrEmail)
            ?? await userManager.FindByNameAsync(loginInfo.UsernameOrEmail);

        if (user is null)
            return NotFound($"User with email or username {loginInfo.UsernameOrEmail} does not exist");
        if (await userManager.CheckPasswordAsync(user, loginInfo.Password))
            return Ok();
        else
            return BadRequest("Incorrect Password");
    }
}
