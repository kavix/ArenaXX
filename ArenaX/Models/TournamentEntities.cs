using System;

namespace ArenaX.Models
{
    public class Tournament
    {
        public int TournamentID { get; set; }
        public string TournamentName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; } = "Planning";
        public string TournamentFormat { get; set; } = "Knockout";
        public int? MaxTeams { get; set; }
        public string? Location { get; set; }
    }

    public class Team
    {
        public int TeamID { get; set; }
        public int? TournamentID { get; set; }
        public string TeamName { get; set; } = string.Empty;
        public string? University { get; set; }
        public string CaptainName { get; set; } = string.Empty;
        public string CaptainEmail { get; set; } = string.Empty;
        public string? CaptainPhone { get; set; }
        public string ApprovalStatus { get; set; } = "Pending";
        public string? TeamDescription { get; set; }
    }

    public class Robot
    {
        public int RobotID { get; set; }
        public int TeamID { get; set; }
        public string RobotName { get; set; } = string.Empty;
        public decimal Weight { get; set; }
        public string Category { get; set; } = string.Empty;
        public string? WeaponType { get; set; }
        public string? Description { get; set; }
        public bool IsApproved { get; set; }
    }

    public class Match
    {
        public int MatchID { get; set; }
        public int TournamentID { get; set; }
        public int Round { get; set; }
        public int MatchNumber { get; set; }
        public int TeamA_ID { get; set; }
        public int TeamB_ID { get; set; }
        public DateTime? ScheduledTime { get; set; }
        public string MatchStatus { get; set; } = "Scheduled";
        public int? WinnerTeamID { get; set; }
    }

    public class Score
    {
        public int ScoreID { get; set; }
        public int MatchID { get; set; }
        public int JudgeID { get; set; }
        public int TeamID { get; set; }
        public int Points { get; set; }
        public int Penalties { get; set; }
        public string? ScoreNotes { get; set; }
    }

    public class RobotDetail : Robot
    {
        public string TeamName { get; set; } = string.Empty;
    }
}
