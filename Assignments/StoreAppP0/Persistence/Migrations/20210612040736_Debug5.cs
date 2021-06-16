using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class Debug5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Orders__location__4BAC3F29",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "LocationProductInventories");

            migrationBuilder.DropColumn(
                name: "orderTotal",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "productPrice",
                table: "Products",
                newName: "ProductPrice");

            migrationBuilder.RenameColumn(
                name: "productName",
                table: "Products",
                newName: "ProductName");

            migrationBuilder.RenameColumn(
                name: "productDescription",
                table: "Products",
                newName: "ProductDescription");

            migrationBuilder.RenameColumn(
                name: "productID",
                table: "Products",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "locationID",
                table: "Orders",
                newName: "LocationId");

            migrationBuilder.RenameColumn(
                name: "orderID",
                table: "Orders",
                newName: "OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_locationID",
                table: "Orders",
                newName: "IX_Orders_LocationId");

            migrationBuilder.RenameColumn(
                name: "locationName",
                table: "Locations",
                newName: "LocationName");

            migrationBuilder.RenameColumn(
                name: "locationID",
                table: "Locations",
                newName: "LocationId");

            migrationBuilder.RenameColumn(
                name: "userName",
                table: "Customers",
                newName: "Username");

            migrationBuilder.RenameColumn(
                name: "passwordHash",
                table: "Customers",
                newName: "PasswordHash");

            migrationBuilder.RenameColumn(
                name: "lastName",
                table: "Customers",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "firstName",
                table: "Customers",
                newName: "FirstName");

            migrationBuilder.RenameColumn(
                name: "defaultLocationID",
                table: "Customers",
                newName: "DefaultLocationId");

            migrationBuilder.RenameColumn(
                name: "customerID",
                table: "Customers",
                newName: "CustomerId");

            migrationBuilder.AlterColumn<string>(
                name: "ProductName",
                table: "Products",
                type: "varchar(25)",
                unicode: false,
                maxLength: 25,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(25)",
                oldUnicode: false,
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ProductDescription",
                table: "Products",
                type: "varchar(200)",
                unicode: false,
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(250)",
                oldUnicode: false,
                oldMaxLength: 250,
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ProductId",
                table: "Products",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "(newid())",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "OrderId",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "(newid())",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "CustomerId",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastOrderDate",
                table: "Orders",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "OrderCreationDate",
                table: "Orders",
                type: "datetime",
                nullable: false,
                defaultValueSql: "(getdate())");

            migrationBuilder.AlterColumn<string>(
                name: "LocationName",
                table: "Locations",
                type: "varchar(25)",
                unicode: false,
                maxLength: 25,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(25)",
                oldUnicode: false,
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "LocationId",
                table: "Locations",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "(newid())",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<string>(
                name: "AddressCity",
                table: "Locations",
                type: "varchar(25)",
                unicode: false,
                maxLength: 25,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AddressState",
                table: "Locations",
                type: "varchar(2)",
                unicode: false,
                maxLength: 2,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AddressStreet",
                table: "Locations",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Locations",
                type: "varchar(25)",
                unicode: false,
                maxLength: 25,
                nullable: true);

            migrationBuilder.AlterColumn<byte[]>(
                name: "PasswordHash",
                table: "Customers",
                type: "binary(64)",
                fixedLength: true,
                maxLength: 64,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldUnicode: false,
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<Guid>(
                name: "DefaultLocationId",
                table: "Customers",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CustomerId",
                table: "Customers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "(newid())",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<DateTime>(
                name: "AccountCreationDate",
                table: "Customers",
                type: "datetime",
                nullable: false,
                defaultValueSql: "(getdate())");

            migrationBuilder.AddColumn<string>(
                name: "AddressCity",
                table: "Customers",
                type: "varchar(25)",
                unicode: false,
                maxLength: 25,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AddressState",
                table: "Customers",
                type: "varchar(2)",
                unicode: false,
                maxLength: 2,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AddressStreet",
                table: "Customers",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Customers",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "LocationProductInventoryJunction",
                columns: table => new
                {
                    LocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SaleDiscount = table.Column<double>(type: "float", nullable: true),
                    ItemsPerOrder = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((1))"),
                    TotalItems = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK__LocationP__Locat__44FF419A",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "LocationId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__LocationP__Produ__45F365D3",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderProductInventoryJunction",
                columns: table => new
                {
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TotalItems = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK__OrderProd__Order__48CFD27E",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__OrderProd__Produ__49C3F6B7",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_LocationProductInventoryJunction_LocationId",
                table: "LocationProductInventoryJunction",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_LocationProductInventoryJunction_ProductId",
                table: "LocationProductInventoryJunction",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderProductInventoryJunction_OrderId",
                table: "OrderProductInventoryJunction",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderProductInventoryJunction_ProductId",
                table: "OrderProductInventoryJunction",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK__Orders__Customer__4222D4EF",
                table: "Orders",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK__Orders__Location__412EB0B6",
                table: "Orders",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "LocationId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Orders__Customer__4222D4EF",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK__Orders__Location__412EB0B6",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "LocationProductInventoryJunction");

            migrationBuilder.DropTable(
                name: "OrderProductInventoryJunction");

            migrationBuilder.DropIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "LastOrderDate",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OrderCreationDate",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "AddressCity",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "AddressState",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "AddressStreet",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "AccountCreationDate",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "AddressCity",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "AddressState",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "AddressStreet",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Customers");

            migrationBuilder.RenameColumn(
                name: "ProductPrice",
                table: "Products",
                newName: "productPrice");

            migrationBuilder.RenameColumn(
                name: "ProductName",
                table: "Products",
                newName: "productName");

            migrationBuilder.RenameColumn(
                name: "ProductDescription",
                table: "Products",
                newName: "productDescription");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "Products",
                newName: "productID");

            migrationBuilder.RenameColumn(
                name: "LocationId",
                table: "Orders",
                newName: "locationID");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "Orders",
                newName: "orderID");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_LocationId",
                table: "Orders",
                newName: "IX_Orders_locationID");

            migrationBuilder.RenameColumn(
                name: "LocationName",
                table: "Locations",
                newName: "locationName");

            migrationBuilder.RenameColumn(
                name: "LocationId",
                table: "Locations",
                newName: "locationID");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "Customers",
                newName: "userName");

            migrationBuilder.RenameColumn(
                name: "PasswordHash",
                table: "Customers",
                newName: "passwordHash");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Customers",
                newName: "lastName");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Customers",
                newName: "firstName");

            migrationBuilder.RenameColumn(
                name: "DefaultLocationId",
                table: "Customers",
                newName: "defaultLocationID");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "Customers",
                newName: "customerID");

            migrationBuilder.AlterColumn<string>(
                name: "productName",
                table: "Products",
                type: "varchar(25)",
                unicode: false,
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(25)",
                oldUnicode: false,
                oldMaxLength: 25);

            migrationBuilder.AlterColumn<string>(
                name: "productDescription",
                table: "Products",
                type: "varchar(250)",
                unicode: false,
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldUnicode: false,
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "productID",
                table: "Products",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValueSql: "(newid())");

            migrationBuilder.AlterColumn<Guid>(
                name: "orderID",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValueSql: "(newid())");

            migrationBuilder.AddColumn<int>(
                name: "orderTotal",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "locationName",
                table: "Locations",
                type: "varchar(25)",
                unicode: false,
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(25)",
                oldUnicode: false,
                oldMaxLength: 25);

            migrationBuilder.AlterColumn<Guid>(
                name: "locationID",
                table: "Locations",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValueSql: "(newid())");

            migrationBuilder.AlterColumn<string>(
                name: "passwordHash",
                table: "Customers",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "binary(64)",
                oldFixedLength: true,
                oldMaxLength: 64);

            migrationBuilder.AlterColumn<int>(
                name: "defaultLocationID",
                table: "Customers",
                type: "int",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "customerID",
                table: "Customers",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValueSql: "(newid())");

            migrationBuilder.CreateTable(
                name: "LocationProductInventories",
                columns: table => new
                {
                    locationID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    productID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    totalProduct = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK__LocationP__locat__5070F446",
                        column: x => x.locationID,
                        principalTable: "Locations",
                        principalColumn: "locationID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__LocationP__produ__4F7CD00D",
                        column: x => x.productID,
                        principalTable: "Products",
                        principalColumn: "productID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LocationProductInventories_locationID",
                table: "LocationProductInventories",
                column: "locationID");

            migrationBuilder.CreateIndex(
                name: "IX_LocationProductInventories_productID",
                table: "LocationProductInventories",
                column: "productID");

            migrationBuilder.AddForeignKey(
                name: "FK__Orders__location__4BAC3F29",
                table: "Orders",
                column: "locationID",
                principalTable: "Locations",
                principalColumn: "locationID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
