using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Subscriptions;
using HotChocolate.Types;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RibbleChatServer.Data;
using RibbleChatServer.GraphQL.ResultTypes;
using RibbleChatServer.Models;

namespace RibbleChatServer.GraphQL
{
    public class MutationType : ObjectType<Mutation>
    {
    }

    public partial class Mutation
    {

        public async Task<ILoginResult> Login(
            string usernameOrEmail,
            string password,
            [Service] UserManager<User> userManager,
            [Service] SignInManager<User> signinManager,
            [Service] MainDbContext dbContext)
        {

            var user = await userManager.FindByEmailAsync(usernameOrEmail)
                ?? await userManager.FindByNameAsync(usernameOrEmail);

            if (user is null) return new LoginUnknownUserError(usernameOrEmail);

            var loginResult = await signinManager.PasswordSignInAsync(user, password, false, false);
            if (!loginResult.Succeeded) return new LoginIncorrectPasswordError();

            var loadedUser = await dbContext.Users
                .Include(user => user.Groups)
                .SingleAsync(u => u.Id == user.Id);
            return new LoginSuccess(loadedUser);

        }
        public record TestMutationInput(int x);
        public record TestMutationPayload(int y);

        public async Task<TestMutationPayload> TestMutation(
            TestMutationInput input,
            [Service] ITopicEventSender eventSender
        )
        {
            await eventSender.SendAsync(new Topic.Test(), input.x);
            return new TestMutationPayload(input.x);
        }
    }
}