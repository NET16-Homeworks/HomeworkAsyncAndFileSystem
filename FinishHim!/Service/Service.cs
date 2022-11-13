using FinishHim_.Models;
using System.Text;
using System.Text.Json;

namespace FinishHim_.Service
{
    public static class Service
    {
        private const string playerPath = "StaticFiles/Players.json";
        private const string teamPath = "StaticFiles/Teams.json";

        public async static Task<List<PlayerModel>> GetPlayer()
        {
            var stringWithPlayers = await File.ReadAllTextAsync(playerPath);
            var listWithPlayers = JsonSerializer.Deserialize<List<PlayerModel>>(stringWithPlayers);
            return listWithPlayers;
        }

        public async static Task<List<TeamModel>> GetTeam()
        {
            var stringWithTeams = await File.ReadAllTextAsync(teamPath);
            var listWithTeams = JsonSerializer.Deserialize<List<TeamModel>>(stringWithTeams);
            return listWithTeams;
        }
        public async static Task AddPlayer(PlayerModel player)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), playerPath);
            var writeStream = File.Open(filePath, FileMode.Open);
            var customerString = ",\n" + JsonSerializer.Serialize(player) + "\n]";
            byte[] buffer = Encoding.Default.GetBytes(customerString);
            writeStream.Seek(-1, SeekOrigin.End);
            await writeStream.WriteAsync(buffer, 0, buffer.Length);
            writeStream.Close();
        }

        public async static Task AddTeam(TeamModel team)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), teamPath);
            var writeStream = File.Open(filePath, FileMode.Open);
            var teamString = ",\n" + JsonSerializer.Serialize(team) + "\n]";
            byte[] buffer = Encoding.Default.GetBytes(teamString);
            writeStream.Seek(-1, SeekOrigin.End);
            await writeStream.WriteAsync(buffer, 0, buffer.Length);
            writeStream.Close();
        }

        public async static Task DeletePlayer(int playerId)
        {
            var stringWithPlayers = await File.ReadAllTextAsync(playerPath);
            var listWithPlayers = JsonSerializer.Deserialize<List<PlayerModel>>(stringWithPlayers);
            var player = listWithPlayers.Where(q => q.PlayerId == playerId).SingleOrDefault();
            listWithPlayers.Remove(player);
            var changedPlayerString = JsonSerializer.Serialize(listWithPlayers);
            await File.WriteAllTextAsync(playerPath, changedPlayerString);
        }

        public async static Task DeleteTeam(int teamNumber)
        {
            var stringWithTeams = await File.ReadAllTextAsync(teamPath);
            var listWithTeams = JsonSerializer.Deserialize<List<TeamModel>>(stringWithTeams);
            var team = listWithTeams.Where(q => q.TeamNumber == teamNumber).SingleOrDefault();
            listWithTeams.Remove(team);
            var changedTeamsString = JsonSerializer.Serialize(listWithTeams);
            await File.WriteAllTextAsync(teamPath, changedTeamsString);
        }
    }
}
