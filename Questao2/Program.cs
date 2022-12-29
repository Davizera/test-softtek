using System.Net.Http.Formatting;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Questao2;

public class Program
{
    public static async Task Main()
    {
        string teamName = "Paris Saint-Germain";
        int year = 2013;
        int totalGoals = await GetTotalScoredGoals(teamName, year);

        Console.WriteLine("Team " + teamName + " scored " + totalGoals.ToString() + " goals in " + year);

        teamName = "Chelsea";
        year = 2014;
        totalGoals = await GetTotalScoredGoals(teamName, year);

        Console.WriteLine("Team " + teamName + " scored " + totalGoals.ToString() + " goals in " + year);

        // Output expected:
        // Team Paris Saint - Germain scored 109 goals in 2013
        // Team Chelsea scored 92 goals in 2014
    }

    static async Task<int> GetTotalScoredGoals(string team, int year)
    {
        var totalGoals = 0;

        HttpClient client = new()
        {
            BaseAddress = new Uri(" https://jsonmock.hackerrank.com/api/"),
        };

        totalGoals += await GetGoalPlayingAtHome(team, year, client);

        totalGoals += await GetGoalsPlayingAwayFromHome(team, year, client);

        return totalGoals;
    }

    private static async Task<int> GetGoalsPlayingAwayFromHome(string team, int year, HttpClient client)
    {
        var totalGoals = 0;
        Dictionary<string, string> query = new()
        {
            {"team2", team},
            {"page", "1"},
            {"year", year.ToString()}
        };

        var response = await client.GetWithQueryString("football_matches", query);
        var footballMatchesResponse = JsonConvert.DeserializeObject<FootballMatchesResponse>(response)!;

        while (footballMatchesResponse.Page <= footballMatchesResponse.TotalPages)
        {
            totalGoals = footballMatchesResponse.Matches.Aggregate(totalGoals,
                (current, match) => current + Convert.ToInt16(match.Team2Goals));
            query["page"] = Convert.ToString(footballMatchesResponse.Page + 1);
            response = await client.GetWithQueryString("football_matches", query);
            footballMatchesResponse =
                JsonConvert.DeserializeObject<FootballMatchesResponse>(response)!;
        }

        return totalGoals;
    }

    private static async Task<int> GetGoalPlayingAtHome(string team, int year, HttpClient client)
    {
        var totalGoals = 0;
        Dictionary<string, string> query = new()
        {
            {"team1", team},
            {"page", "1"},
            {"year", year.ToString()}
        };

        var response = await client.GetWithQueryString("football_matches", query);
        var footballMatchesResponse =
            JsonConvert.DeserializeObject<FootballMatchesResponse>(response)!;

        while (footballMatchesResponse.Page <= footballMatchesResponse.TotalPages)
        {
            totalGoals = footballMatchesResponse.Matches.Aggregate(totalGoals,
                (current, match) => current + Convert.ToInt16(match.Team1Goals));
            query["page"] = Convert.ToString(footballMatchesResponse.Page + 1);
            response = await client.GetWithQueryString("football_matches", query);
            footballMatchesResponse =
                JsonConvert.DeserializeObject<FootballMatchesResponse>(response)!;
        }

        return totalGoals;
    }
}