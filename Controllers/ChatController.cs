using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RibbleChatServer.Data;
using RibbleChatServer.Models;

[ApiController]
public class ChatController : ControllerBase
{
    private readonly ChatDbContext dbContext;
    private readonly UserManager<User> userManager;

    public ChatController(ChatDbContext dbContext, UserManager<User> userManager)
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
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<User>> Register([FromBody] RegisterUserInfo userInfo)
    {
        System.Console.WriteLine("hello");
        System.Console.WriteLine(userInfo);
        var user = new User
        {
            FirstName = userInfo.FirstName,
            LastName = userInfo.LastName,
            Email = userInfo.Email,
        };

        var userCreationResult = await userManager.CreateAsync(user, userInfo.Password);
        if (userCreationResult.Succeeded) return Created("", user);
        return Problem(userCreationResult.Errors.First().Description, null, 422);
    }

    [HttpPost]
    [Route("/api/auth")]
    public async Task<IActionResult> Login(LoginUserInfo loginInfo)
    {
        var user = userManager.Users.SingleOrDefault(u =>
            u.UserName == loginInfo.UsernameOrEmail || u.Email == loginInfo.UsernameOrEmail);
        if (user is null) return NotFound($"User with email or username {loginInfo} does not exist");
        if (await userManager.CheckPasswordAsync(user, loginInfo.Password)) return Ok();
        else return BadRequest("Incorrect Password");
    }
}