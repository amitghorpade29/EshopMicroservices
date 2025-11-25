
using Catalog.API.Products.UpdateProduct;

namespace Catalog.API.Products.DeleteProduct
{
    public record DeleteProductCommnad(Guid Id) : ICommand<DeleteProductResult>;

    public record DeleteProductResult(bool IsSucess);

    public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommnad>
    {
        public DeleteProductCommandValidator()
        {
            RuleFor(command => command.Id).NotEmpty().WithMessage("Product ID is required!");
        }
    }

    internal class DeleteProductCOmmandHandler (IDocumentSession session, ILogger<DeleteProductCOmmandHandler> logger)
        : ICommandHandler<DeleteProductCommnad, DeleteProductResult>
    {
        public async Task<DeleteProductResult> Handle(DeleteProductCommnad command, CancellationToken cancellationToken)
        {
            session.Delete<Product>(command.Id);
            await session.SaveChangesAsync(cancellationToken);
            return new DeleteProductResult(true);
        }
    }
}
