﻿namespace NetMovilAPI.Application.DTOs.Requests;

public class ProductRequestDTO
{
    public int ProductID { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal BasePrice { get; set; }
    public decimal ProfitMargin { get; set; }
    public decimal UnitPrice { get; set; }
    public string ImageUrl { get; set; } = "defaultproductimage.png";
    public string? BarCode { get; set; } = "Sin Código de Barras";
    public bool IsStock { get; set; }
    public decimal? StockQuantity { get; set; } = new();
    public decimal? Threshold { get; set; }
    public int ProductStatusID { get; set; }
    public int CreatedBy { get; set; }
    public int[]? ProductCategories { get; set; }
    public int BranchID { get; set; } // NUEVO CAMPO
}