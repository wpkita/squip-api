using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Squip.Rest.Migrations
{
    /// <inheritdoc />
    public partial class SnakeCase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Ideas_LeftId",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "FK_Games_Ideas_LoserId",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "FK_Games_Ideas_RightId",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "FK_Games_Ideas_WinnerId",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "FK_Games_Users_UserId",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "FK_Habits_Users_UserId",
                table: "Habits");

            migrationBuilder.DropForeignKey(
                name: "FK_Hibits_Habits_HabitId",
                table: "Hibits");

            migrationBuilder.DropForeignKey(
                name: "FK_Hibits_Users_UserId",
                table: "Hibits");

            migrationBuilder.DropForeignKey(
                name: "FK_Ideas_Users_UserId",
                table: "Ideas");

            migrationBuilder.DropForeignKey(
                name: "FK_Logs_Users_UserId",
                table: "Logs");

            migrationBuilder.DropForeignKey(
                name: "FK_MoodEntries_Moods_MoodId",
                table: "MoodEntries");

            migrationBuilder.DropForeignKey(
                name: "FK_MoodEntries_Users_UserId",
                table: "MoodEntries");

            migrationBuilder.DropForeignKey(
                name: "FK_Moods_Users_UserId",
                table: "Moods");

            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Ideas_IdeaId",
                table: "Tags");

            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Users_UserId",
                table: "Tags");

            migrationBuilder.DropForeignKey(
                name: "FK_TargetEntries_Targets_TargetId",
                table: "TargetEntries");

            migrationBuilder.DropForeignKey(
                name: "FK_TargetEntries_Users_UserId",
                table: "TargetEntries");

            migrationBuilder.DropForeignKey(
                name: "FK_Targets_Users_UserId",
                table: "Targets");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Users_OidcSub",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Targets",
                table: "Targets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tags",
                table: "Tags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Moods",
                table: "Moods");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Logs",
                table: "Logs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ideas",
                table: "Ideas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Hibits",
                table: "Hibits");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Habits",
                table: "Habits");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Games",
                table: "Games");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TargetEntries",
                table: "TargetEntries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MoodEntries",
                table: "MoodEntries");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "users");

            migrationBuilder.RenameTable(
                name: "Targets",
                newName: "targets");

            migrationBuilder.RenameTable(
                name: "Tags",
                newName: "tags");

            migrationBuilder.RenameTable(
                name: "Moods",
                newName: "moods");

            migrationBuilder.RenameTable(
                name: "Logs",
                newName: "logs");

            migrationBuilder.RenameTable(
                name: "Ideas",
                newName: "ideas");

            migrationBuilder.RenameTable(
                name: "Hibits",
                newName: "hibits");

            migrationBuilder.RenameTable(
                name: "Habits",
                newName: "habits");

            migrationBuilder.RenameTable(
                name: "Games",
                newName: "games");

            migrationBuilder.RenameTable(
                name: "TargetEntries",
                newName: "target_entries");

            migrationBuilder.RenameTable(
                name: "MoodEntries",
                newName: "mood_entries");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "users",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "OidcSub",
                table: "users",
                newName: "oidc_sub");

            migrationBuilder.RenameColumn(
                name: "InstantUpdatedAt",
                table: "users",
                newName: "instant_updated_at");

            migrationBuilder.RenameColumn(
                name: "InstantCreatedAt",
                table: "users",
                newName: "instant_created_at");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "targets",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "targets",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "targets",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "IsArchived",
                table: "targets",
                newName: "is_archived");

            migrationBuilder.RenameColumn(
                name: "InstantUpdatedAt",
                table: "targets",
                newName: "instant_updated_at");

            migrationBuilder.RenameColumn(
                name: "InstantCreatedAt",
                table: "targets",
                newName: "instant_created_at");

            migrationBuilder.RenameIndex(
                name: "IX_Targets_UserId",
                table: "targets",
                newName: "ix_targets_user_id");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "tags",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "tags",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "tags",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "IsArchived",
                table: "tags",
                newName: "is_archived");

            migrationBuilder.RenameColumn(
                name: "InstantUpdatedAt",
                table: "tags",
                newName: "instant_updated_at");

            migrationBuilder.RenameColumn(
                name: "InstantCreatedAt",
                table: "tags",
                newName: "instant_created_at");

            migrationBuilder.RenameColumn(
                name: "IdeaId",
                table: "tags",
                newName: "idea_id");

            migrationBuilder.RenameIndex(
                name: "IX_Tags_Name",
                table: "tags",
                newName: "ix_tags_name");

            migrationBuilder.RenameIndex(
                name: "IX_Tags_UserId",
                table: "tags",
                newName: "ix_tags_user_id");

            migrationBuilder.RenameIndex(
                name: "IX_Tags_IdeaId",
                table: "tags",
                newName: "ix_tags_idea_id");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "moods",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "moods",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "moods",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "IsArchived",
                table: "moods",
                newName: "is_archived");

            migrationBuilder.RenameColumn(
                name: "InstantUpdatedAt",
                table: "moods",
                newName: "instant_updated_at");

            migrationBuilder.RenameColumn(
                name: "InstantCreatedAt",
                table: "moods",
                newName: "instant_created_at");

            migrationBuilder.RenameIndex(
                name: "IX_Moods_UserId",
                table: "moods",
                newName: "ix_moods_user_id");

            migrationBuilder.RenameColumn(
                name: "Message",
                table: "logs",
                newName: "message");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "logs",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "logs",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "InstantUpdatedAt",
                table: "logs",
                newName: "instant_updated_at");

            migrationBuilder.RenameColumn(
                name: "InstantCreatedAt",
                table: "logs",
                newName: "instant_created_at");

            migrationBuilder.RenameIndex(
                name: "IX_Logs_UserId",
                table: "logs",
                newName: "ix_logs_user_id");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "ideas",
                newName: "title");

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "ideas",
                newName: "content");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ideas",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "ideas",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "IsArchived",
                table: "ideas",
                newName: "is_archived");

            migrationBuilder.RenameColumn(
                name: "InstantUpdatedAt",
                table: "ideas",
                newName: "instant_updated_at");

            migrationBuilder.RenameColumn(
                name: "InstantCreatedAt",
                table: "ideas",
                newName: "instant_created_at");

            migrationBuilder.RenameColumn(
                name: "EloRating",
                table: "ideas",
                newName: "elo_rating");

            migrationBuilder.RenameIndex(
                name: "IX_Ideas_UserId",
                table: "ideas",
                newName: "ix_ideas_user_id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "hibits",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "hibits",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "IsArchived",
                table: "hibits",
                newName: "is_archived");

            migrationBuilder.RenameColumn(
                name: "InstantUpdatedAt",
                table: "hibits",
                newName: "instant_updated_at");

            migrationBuilder.RenameColumn(
                name: "InstantOccurredAt",
                table: "hibits",
                newName: "instant_occurred_at");

            migrationBuilder.RenameColumn(
                name: "InstantCreatedAt",
                table: "hibits",
                newName: "instant_created_at");

            migrationBuilder.RenameColumn(
                name: "HabitId",
                table: "hibits",
                newName: "habit_id");

            migrationBuilder.RenameIndex(
                name: "IX_Hibits_UserId",
                table: "hibits",
                newName: "ix_hibits_user_id");

            migrationBuilder.RenameIndex(
                name: "IX_Hibits_HabitId",
                table: "hibits",
                newName: "ix_hibits_habit_id");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "habits",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "habits",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "habits",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "IsArchived",
                table: "habits",
                newName: "is_archived");

            migrationBuilder.RenameColumn(
                name: "InstantUpdatedAt",
                table: "habits",
                newName: "instant_updated_at");

            migrationBuilder.RenameColumn(
                name: "InstantCreatedAt",
                table: "habits",
                newName: "instant_created_at");

            migrationBuilder.RenameIndex(
                name: "IX_Habits_UserId",
                table: "habits",
                newName: "ix_habits_user_id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "games",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "WinnerId",
                table: "games",
                newName: "winner_id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "games",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "RightId",
                table: "games",
                newName: "right_id");

            migrationBuilder.RenameColumn(
                name: "LoserId",
                table: "games",
                newName: "loser_id");

            migrationBuilder.RenameColumn(
                name: "LeftId",
                table: "games",
                newName: "left_id");

            migrationBuilder.RenameColumn(
                name: "InstantUpdatedAt",
                table: "games",
                newName: "instant_updated_at");

            migrationBuilder.RenameColumn(
                name: "InstantCreatedAt",
                table: "games",
                newName: "instant_created_at");

            migrationBuilder.RenameIndex(
                name: "IX_Games_WinnerId",
                table: "games",
                newName: "ix_games_winner_id");

            migrationBuilder.RenameIndex(
                name: "IX_Games_UserId",
                table: "games",
                newName: "ix_games_user_id");

            migrationBuilder.RenameIndex(
                name: "IX_Games_RightId",
                table: "games",
                newName: "ix_games_right_id");

            migrationBuilder.RenameIndex(
                name: "IX_Games_LoserId",
                table: "games",
                newName: "ix_games_loser_id");

            migrationBuilder.RenameIndex(
                name: "IX_Games_LeftId",
                table: "games",
                newName: "ix_games_left_id");

            migrationBuilder.RenameColumn(
                name: "Magnitude",
                table: "target_entries",
                newName: "magnitude");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "target_entries",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "target_entries",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "TargetId",
                table: "target_entries",
                newName: "target_id");

            migrationBuilder.RenameColumn(
                name: "IsArchived",
                table: "target_entries",
                newName: "is_archived");

            migrationBuilder.RenameColumn(
                name: "InstantUpdatedAt",
                table: "target_entries",
                newName: "instant_updated_at");

            migrationBuilder.RenameColumn(
                name: "InstantOccurredAt",
                table: "target_entries",
                newName: "instant_occurred_at");

            migrationBuilder.RenameColumn(
                name: "InstantCreatedAt",
                table: "target_entries",
                newName: "instant_created_at");

            migrationBuilder.RenameColumn(
                name: "DidEngage",
                table: "target_entries",
                newName: "did_engage");

            migrationBuilder.RenameIndex(
                name: "IX_TargetEntries_UserId",
                table: "target_entries",
                newName: "ix_target_entries_user_id");

            migrationBuilder.RenameIndex(
                name: "IX_TargetEntries_TargetId",
                table: "target_entries",
                newName: "ix_target_entries_target_id");

            migrationBuilder.RenameColumn(
                name: "Magnitude",
                table: "mood_entries",
                newName: "magnitude");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "mood_entries",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "mood_entries",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "MoodId",
                table: "mood_entries",
                newName: "mood_id");

            migrationBuilder.RenameColumn(
                name: "IsArchived",
                table: "mood_entries",
                newName: "is_archived");

            migrationBuilder.RenameColumn(
                name: "InstantUpdatedAt",
                table: "mood_entries",
                newName: "instant_updated_at");

            migrationBuilder.RenameColumn(
                name: "InstantOccurredAt",
                table: "mood_entries",
                newName: "instant_occurred_at");

            migrationBuilder.RenameColumn(
                name: "InstantCreatedAt",
                table: "mood_entries",
                newName: "instant_created_at");

            migrationBuilder.RenameIndex(
                name: "IX_MoodEntries_UserId",
                table: "mood_entries",
                newName: "ix_mood_entries_user_id");

            migrationBuilder.RenameIndex(
                name: "IX_MoodEntries_MoodId",
                table: "mood_entries",
                newName: "ix_mood_entries_mood_id");

            migrationBuilder.AddUniqueConstraint(
                name: "ak_users_oidc_sub",
                table: "users",
                column: "oidc_sub");

            migrationBuilder.AddPrimaryKey(
                name: "pk_users",
                table: "users",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_targets",
                table: "targets",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_tags",
                table: "tags",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_moods",
                table: "moods",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_logs",
                table: "logs",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_ideas",
                table: "ideas",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_hibits",
                table: "hibits",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_habits",
                table: "habits",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_games",
                table: "games",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_target_entries",
                table: "target_entries",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_mood_entries",
                table: "mood_entries",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_games_ideas_left_id",
                table: "games",
                column: "left_id",
                principalTable: "ideas",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_games_ideas_loser_id",
                table: "games",
                column: "loser_id",
                principalTable: "ideas",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_games_ideas_right_id",
                table: "games",
                column: "right_id",
                principalTable: "ideas",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_games_ideas_winner_id",
                table: "games",
                column: "winner_id",
                principalTable: "ideas",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_games_users_user_id",
                table: "games",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_habits_users_user_id",
                table: "habits",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_hibits_habits_habit_id",
                table: "hibits",
                column: "habit_id",
                principalTable: "habits",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_hibits_users_user_id",
                table: "hibits",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_ideas_users_user_id",
                table: "ideas",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_logs_users_user_id",
                table: "logs",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_mood_entries_moods_mood_id",
                table: "mood_entries",
                column: "mood_id",
                principalTable: "moods",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_mood_entries_users_user_id",
                table: "mood_entries",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_moods_users_user_id",
                table: "moods",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_tags_ideas_idea_id",
                table: "tags",
                column: "idea_id",
                principalTable: "ideas",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_tags_users_user_id",
                table: "tags",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_target_entries_targets_target_id",
                table: "target_entries",
                column: "target_id",
                principalTable: "targets",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_target_entries_users_user_id",
                table: "target_entries",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_targets_users_user_id",
                table: "targets",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_games_ideas_left_id",
                table: "games");

            migrationBuilder.DropForeignKey(
                name: "fk_games_ideas_loser_id",
                table: "games");

            migrationBuilder.DropForeignKey(
                name: "fk_games_ideas_right_id",
                table: "games");

            migrationBuilder.DropForeignKey(
                name: "fk_games_ideas_winner_id",
                table: "games");

            migrationBuilder.DropForeignKey(
                name: "fk_games_users_user_id",
                table: "games");

            migrationBuilder.DropForeignKey(
                name: "fk_habits_users_user_id",
                table: "habits");

            migrationBuilder.DropForeignKey(
                name: "fk_hibits_habits_habit_id",
                table: "hibits");

            migrationBuilder.DropForeignKey(
                name: "fk_hibits_users_user_id",
                table: "hibits");

            migrationBuilder.DropForeignKey(
                name: "fk_ideas_users_user_id",
                table: "ideas");

            migrationBuilder.DropForeignKey(
                name: "fk_logs_users_user_id",
                table: "logs");

            migrationBuilder.DropForeignKey(
                name: "fk_mood_entries_moods_mood_id",
                table: "mood_entries");

            migrationBuilder.DropForeignKey(
                name: "fk_mood_entries_users_user_id",
                table: "mood_entries");

            migrationBuilder.DropForeignKey(
                name: "fk_moods_users_user_id",
                table: "moods");

            migrationBuilder.DropForeignKey(
                name: "fk_tags_ideas_idea_id",
                table: "tags");

            migrationBuilder.DropForeignKey(
                name: "fk_tags_users_user_id",
                table: "tags");

            migrationBuilder.DropForeignKey(
                name: "fk_target_entries_targets_target_id",
                table: "target_entries");

            migrationBuilder.DropForeignKey(
                name: "fk_target_entries_users_user_id",
                table: "target_entries");

            migrationBuilder.DropForeignKey(
                name: "fk_targets_users_user_id",
                table: "targets");

            migrationBuilder.DropUniqueConstraint(
                name: "ak_users_oidc_sub",
                table: "users");

            migrationBuilder.DropPrimaryKey(
                name: "pk_users",
                table: "users");

            migrationBuilder.DropPrimaryKey(
                name: "pk_targets",
                table: "targets");

            migrationBuilder.DropPrimaryKey(
                name: "pk_tags",
                table: "tags");

            migrationBuilder.DropPrimaryKey(
                name: "pk_moods",
                table: "moods");

            migrationBuilder.DropPrimaryKey(
                name: "pk_logs",
                table: "logs");

            migrationBuilder.DropPrimaryKey(
                name: "pk_ideas",
                table: "ideas");

            migrationBuilder.DropPrimaryKey(
                name: "pk_hibits",
                table: "hibits");

            migrationBuilder.DropPrimaryKey(
                name: "pk_habits",
                table: "habits");

            migrationBuilder.DropPrimaryKey(
                name: "pk_games",
                table: "games");

            migrationBuilder.DropPrimaryKey(
                name: "pk_target_entries",
                table: "target_entries");

            migrationBuilder.DropPrimaryKey(
                name: "pk_mood_entries",
                table: "mood_entries");

            migrationBuilder.RenameTable(
                name: "users",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "targets",
                newName: "Targets");

            migrationBuilder.RenameTable(
                name: "tags",
                newName: "Tags");

            migrationBuilder.RenameTable(
                name: "moods",
                newName: "Moods");

            migrationBuilder.RenameTable(
                name: "logs",
                newName: "Logs");

            migrationBuilder.RenameTable(
                name: "ideas",
                newName: "Ideas");

            migrationBuilder.RenameTable(
                name: "hibits",
                newName: "Hibits");

            migrationBuilder.RenameTable(
                name: "habits",
                newName: "Habits");

            migrationBuilder.RenameTable(
                name: "games",
                newName: "Games");

            migrationBuilder.RenameTable(
                name: "target_entries",
                newName: "TargetEntries");

            migrationBuilder.RenameTable(
                name: "mood_entries",
                newName: "MoodEntries");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Users",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "oidc_sub",
                table: "Users",
                newName: "OidcSub");

            migrationBuilder.RenameColumn(
                name: "instant_updated_at",
                table: "Users",
                newName: "InstantUpdatedAt");

            migrationBuilder.RenameColumn(
                name: "instant_created_at",
                table: "Users",
                newName: "InstantCreatedAt");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Targets",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Targets",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "Targets",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "is_archived",
                table: "Targets",
                newName: "IsArchived");

            migrationBuilder.RenameColumn(
                name: "instant_updated_at",
                table: "Targets",
                newName: "InstantUpdatedAt");

            migrationBuilder.RenameColumn(
                name: "instant_created_at",
                table: "Targets",
                newName: "InstantCreatedAt");

            migrationBuilder.RenameIndex(
                name: "ix_targets_user_id",
                table: "Targets",
                newName: "IX_Targets_UserId");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Tags",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Tags",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "Tags",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "is_archived",
                table: "Tags",
                newName: "IsArchived");

            migrationBuilder.RenameColumn(
                name: "instant_updated_at",
                table: "Tags",
                newName: "InstantUpdatedAt");

            migrationBuilder.RenameColumn(
                name: "instant_created_at",
                table: "Tags",
                newName: "InstantCreatedAt");

            migrationBuilder.RenameColumn(
                name: "idea_id",
                table: "Tags",
                newName: "IdeaId");

            migrationBuilder.RenameIndex(
                name: "ix_tags_name",
                table: "Tags",
                newName: "IX_Tags_Name");

            migrationBuilder.RenameIndex(
                name: "ix_tags_user_id",
                table: "Tags",
                newName: "IX_Tags_UserId");

            migrationBuilder.RenameIndex(
                name: "ix_tags_idea_id",
                table: "Tags",
                newName: "IX_Tags_IdeaId");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Moods",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Moods",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "Moods",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "is_archived",
                table: "Moods",
                newName: "IsArchived");

            migrationBuilder.RenameColumn(
                name: "instant_updated_at",
                table: "Moods",
                newName: "InstantUpdatedAt");

            migrationBuilder.RenameColumn(
                name: "instant_created_at",
                table: "Moods",
                newName: "InstantCreatedAt");

            migrationBuilder.RenameIndex(
                name: "ix_moods_user_id",
                table: "Moods",
                newName: "IX_Moods_UserId");

            migrationBuilder.RenameColumn(
                name: "message",
                table: "Logs",
                newName: "Message");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Logs",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "Logs",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "instant_updated_at",
                table: "Logs",
                newName: "InstantUpdatedAt");

            migrationBuilder.RenameColumn(
                name: "instant_created_at",
                table: "Logs",
                newName: "InstantCreatedAt");

            migrationBuilder.RenameIndex(
                name: "ix_logs_user_id",
                table: "Logs",
                newName: "IX_Logs_UserId");

            migrationBuilder.RenameColumn(
                name: "title",
                table: "Ideas",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "content",
                table: "Ideas",
                newName: "Content");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Ideas",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "Ideas",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "is_archived",
                table: "Ideas",
                newName: "IsArchived");

            migrationBuilder.RenameColumn(
                name: "instant_updated_at",
                table: "Ideas",
                newName: "InstantUpdatedAt");

            migrationBuilder.RenameColumn(
                name: "instant_created_at",
                table: "Ideas",
                newName: "InstantCreatedAt");

            migrationBuilder.RenameColumn(
                name: "elo_rating",
                table: "Ideas",
                newName: "EloRating");

            migrationBuilder.RenameIndex(
                name: "ix_ideas_user_id",
                table: "Ideas",
                newName: "IX_Ideas_UserId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Hibits",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "Hibits",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "is_archived",
                table: "Hibits",
                newName: "IsArchived");

            migrationBuilder.RenameColumn(
                name: "instant_updated_at",
                table: "Hibits",
                newName: "InstantUpdatedAt");

            migrationBuilder.RenameColumn(
                name: "instant_occurred_at",
                table: "Hibits",
                newName: "InstantOccurredAt");

            migrationBuilder.RenameColumn(
                name: "instant_created_at",
                table: "Hibits",
                newName: "InstantCreatedAt");

            migrationBuilder.RenameColumn(
                name: "habit_id",
                table: "Hibits",
                newName: "HabitId");

            migrationBuilder.RenameIndex(
                name: "ix_hibits_user_id",
                table: "Hibits",
                newName: "IX_Hibits_UserId");

            migrationBuilder.RenameIndex(
                name: "ix_hibits_habit_id",
                table: "Hibits",
                newName: "IX_Hibits_HabitId");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Habits",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Habits",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "Habits",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "is_archived",
                table: "Habits",
                newName: "IsArchived");

            migrationBuilder.RenameColumn(
                name: "instant_updated_at",
                table: "Habits",
                newName: "InstantUpdatedAt");

            migrationBuilder.RenameColumn(
                name: "instant_created_at",
                table: "Habits",
                newName: "InstantCreatedAt");

            migrationBuilder.RenameIndex(
                name: "ix_habits_user_id",
                table: "Habits",
                newName: "IX_Habits_UserId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Games",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "winner_id",
                table: "Games",
                newName: "WinnerId");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "Games",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "right_id",
                table: "Games",
                newName: "RightId");

            migrationBuilder.RenameColumn(
                name: "loser_id",
                table: "Games",
                newName: "LoserId");

            migrationBuilder.RenameColumn(
                name: "left_id",
                table: "Games",
                newName: "LeftId");

            migrationBuilder.RenameColumn(
                name: "instant_updated_at",
                table: "Games",
                newName: "InstantUpdatedAt");

            migrationBuilder.RenameColumn(
                name: "instant_created_at",
                table: "Games",
                newName: "InstantCreatedAt");

            migrationBuilder.RenameIndex(
                name: "ix_games_winner_id",
                table: "Games",
                newName: "IX_Games_WinnerId");

            migrationBuilder.RenameIndex(
                name: "ix_games_user_id",
                table: "Games",
                newName: "IX_Games_UserId");

            migrationBuilder.RenameIndex(
                name: "ix_games_right_id",
                table: "Games",
                newName: "IX_Games_RightId");

            migrationBuilder.RenameIndex(
                name: "ix_games_loser_id",
                table: "Games",
                newName: "IX_Games_LoserId");

            migrationBuilder.RenameIndex(
                name: "ix_games_left_id",
                table: "Games",
                newName: "IX_Games_LeftId");

            migrationBuilder.RenameColumn(
                name: "magnitude",
                table: "TargetEntries",
                newName: "Magnitude");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "TargetEntries",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "TargetEntries",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "target_id",
                table: "TargetEntries",
                newName: "TargetId");

            migrationBuilder.RenameColumn(
                name: "is_archived",
                table: "TargetEntries",
                newName: "IsArchived");

            migrationBuilder.RenameColumn(
                name: "instant_updated_at",
                table: "TargetEntries",
                newName: "InstantUpdatedAt");

            migrationBuilder.RenameColumn(
                name: "instant_occurred_at",
                table: "TargetEntries",
                newName: "InstantOccurredAt");

            migrationBuilder.RenameColumn(
                name: "instant_created_at",
                table: "TargetEntries",
                newName: "InstantCreatedAt");

            migrationBuilder.RenameColumn(
                name: "did_engage",
                table: "TargetEntries",
                newName: "DidEngage");

            migrationBuilder.RenameIndex(
                name: "ix_target_entries_user_id",
                table: "TargetEntries",
                newName: "IX_TargetEntries_UserId");

            migrationBuilder.RenameIndex(
                name: "ix_target_entries_target_id",
                table: "TargetEntries",
                newName: "IX_TargetEntries_TargetId");

            migrationBuilder.RenameColumn(
                name: "magnitude",
                table: "MoodEntries",
                newName: "Magnitude");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "MoodEntries",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "MoodEntries",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "mood_id",
                table: "MoodEntries",
                newName: "MoodId");

            migrationBuilder.RenameColumn(
                name: "is_archived",
                table: "MoodEntries",
                newName: "IsArchived");

            migrationBuilder.RenameColumn(
                name: "instant_updated_at",
                table: "MoodEntries",
                newName: "InstantUpdatedAt");

            migrationBuilder.RenameColumn(
                name: "instant_occurred_at",
                table: "MoodEntries",
                newName: "InstantOccurredAt");

            migrationBuilder.RenameColumn(
                name: "instant_created_at",
                table: "MoodEntries",
                newName: "InstantCreatedAt");

            migrationBuilder.RenameIndex(
                name: "ix_mood_entries_user_id",
                table: "MoodEntries",
                newName: "IX_MoodEntries_UserId");

            migrationBuilder.RenameIndex(
                name: "ix_mood_entries_mood_id",
                table: "MoodEntries",
                newName: "IX_MoodEntries_MoodId");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Users_OidcSub",
                table: "Users",
                column: "OidcSub");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Targets",
                table: "Targets",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tags",
                table: "Tags",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Moods",
                table: "Moods",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Logs",
                table: "Logs",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ideas",
                table: "Ideas",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Hibits",
                table: "Hibits",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Habits",
                table: "Habits",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Games",
                table: "Games",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TargetEntries",
                table: "TargetEntries",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MoodEntries",
                table: "MoodEntries",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Ideas_LeftId",
                table: "Games",
                column: "LeftId",
                principalTable: "Ideas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Ideas_LoserId",
                table: "Games",
                column: "LoserId",
                principalTable: "Ideas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Ideas_RightId",
                table: "Games",
                column: "RightId",
                principalTable: "Ideas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Ideas_WinnerId",
                table: "Games",
                column: "WinnerId",
                principalTable: "Ideas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Users_UserId",
                table: "Games",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Habits_Users_UserId",
                table: "Habits",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Hibits_Habits_HabitId",
                table: "Hibits",
                column: "HabitId",
                principalTable: "Habits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Hibits_Users_UserId",
                table: "Hibits",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ideas_Users_UserId",
                table: "Ideas",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Logs_Users_UserId",
                table: "Logs",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MoodEntries_Moods_MoodId",
                table: "MoodEntries",
                column: "MoodId",
                principalTable: "Moods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MoodEntries_Users_UserId",
                table: "MoodEntries",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Moods_Users_UserId",
                table: "Moods",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Ideas_IdeaId",
                table: "Tags",
                column: "IdeaId",
                principalTable: "Ideas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Users_UserId",
                table: "Tags",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TargetEntries_Targets_TargetId",
                table: "TargetEntries",
                column: "TargetId",
                principalTable: "Targets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TargetEntries_Users_UserId",
                table: "TargetEntries",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Targets_Users_UserId",
                table: "Targets",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
