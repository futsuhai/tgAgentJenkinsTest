using TgInstanceAgent.Domain.Abstractions.Interfaces;
using TgInstanceAgent.Domain.Reports;
using TgInstanceAgent.Domain.Reports.Ordering.Visitor;
using TgInstanceAgent.Domain.Reports.Specifications.Visitor;

namespace TgInstanceAgent.Domain.Abstractions.Repositories;

/// <summary>
/// Интерфейс репозитория для работы с отчета по сообщениям.
/// </summary>
public interface IReportRepository : IRepository<ReportAggregate, Guid, IReportSpecificationVisitor, IReportSortingVisitor>;
