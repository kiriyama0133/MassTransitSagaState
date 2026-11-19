using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MassTransitSagaState.Server.Migrations
{
    /// <summary>
    /// 空迁移：只用于把 NewsletterEmailSagaData 从 AppDbContext 的模型快照中移除。
    /// 表本身的所有权已经移交给 MassTransitSaga.Sagas 的 SagaDbContext，
    /// 由它的 InitialSagaState 迁移负责建表，所以这里绝不能 DropTable。
    /// </summary>
    public partial class SyncAppModelAfterSagaExtraction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 故意留空：saga 表不再由 AppDbContext 管理。
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // 故意留空：回滚模型快照即可，不涉及数据库结构变更。
        }
    }
}
