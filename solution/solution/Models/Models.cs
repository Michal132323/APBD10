namespace solution.DataBase;

public class Role
{
    public int RoleId { get; set; }
    public string Name { get; set; }
    public ICollection<Account> Accounts { get; set; }
}

public class Account
{
    public int AccountId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public int RoleId { get; set; }
    public Role Role { get; set; }
    public ICollection<ShoppingCart> ShoppingCarts { get; set; }
}

public class Product
{
    public int ProductId { get; set; }
    public string Name { get; set; }
    public decimal Weight { get; set; }
    public decimal Width { get; set; }
    public decimal Height { get; set; }
    public decimal Depth { get; set; }
    public ICollection<ProductCategory> ProductCategories { get; set; }
    public ICollection<ShoppingCart> ShoppingCarts { get; set; }
}

public class Category
{
    public int CategoryId { get; set; }
    public string Name { get; set; }
    public ICollection<ProductCategory> ProductCategories { get; set; }
}

public class ProductCategory
{
    public int ProductId { get; set; }
    public Product Product { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
}

public class ShoppingCart
{
    public int AccountId { get; set; }
    public Account Account { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; }
    public int Amount { get; set; }
}