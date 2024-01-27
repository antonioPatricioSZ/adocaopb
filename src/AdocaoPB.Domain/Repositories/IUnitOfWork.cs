namespace AdocaoPB.Domain.Repositories;

public interface IUnitOfWork {

    Task Commit();

}
