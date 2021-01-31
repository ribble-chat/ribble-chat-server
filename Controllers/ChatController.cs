using Microsoft.AspNetCore.Mvc;
using RibbleChatServer.Data;

public class ChatController : Controller
{
    private readonly ChatDbContext dbContext;
    public ChatController(ChatDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
}