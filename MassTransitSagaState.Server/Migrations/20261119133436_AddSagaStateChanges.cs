using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MassTransitSagaState.Server.Migrations
{
    /// <summary>
    /// 历史迁移，原本负责把 NewsletterEmailSagaData 重命名为 NewsletterEmailSagaDatas。
    /// saga 表已移交给 MassTransitSaga.Sagas 的 SagaDbContext，这里不再做任何数据库操作，
    /// 仅保留在迁移链中以维持已应用数据库的历史顺序。
    /// </summary>
    public partial class AddSagaStateChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 故意留空：saga 表不再由 AppDbContext 的迁移链管理。
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // 故意留空：同上。
        }
    }
}
