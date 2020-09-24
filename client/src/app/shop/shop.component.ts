import { Component, OnInit } from "@angular/core";
import { IBrand } from "../shared/models/brand";
import { IPagination } from "../shared/models/pagination";
import { IProduct } from "../shared/models/product";
import { IType } from "../shared/models/productType";
import { ShopService } from "./shop.service";

@Component({
  selector: "app-shop",
  templateUrl: "./shop.component.html",
  styleUrls: ["./shop.component.scss"],
})
export class ShopComponent implements OnInit {
  products: IProduct[];
  brands: IBrand[];
  types: IType[];
  brandIdSelected: number = 0;
  typeIdSelected: number = 0;
  sortPrice: string = "";
  sortOptions: any[] = [
    { name: 'Alphabetically', value: 'Alphabetically' },
    { name: 'Price: Low to High', value: 'priceAsc' },
    { name: 'Price: High to Low', value: 'priceDesc' }
  ];

  constructor(private shopService: ShopService) {}

  ngOnInit(): void {
    this.getProducts();
    this.getTypes();
    this.getBrands();
  }

  getProducts(): void {
    this.shopService
      .getProducts(this.brandIdSelected, this.typeIdSelected, this.sortPrice)
      .subscribe(
        (response: IPagination<IProduct>) => {
          this.products = response.data;
        },
        (error) => {
          console.log(error);
        }
      );
  }

  getBrands() {
    this.shopService.getBrands().subscribe(
      (response: IBrand[]) => {
        this.brands = [{ id: 0, name: "All" }, ...response];
      },
      (error) => {
        console.log(error);
      }
    );
  }

  getTypes() {
    this.shopService.getTypes().subscribe(
      (response: IType[]) => {
        this.types = [{ id: 0, name: "All" }, ...response];
      },
      (error) => {
        console.log(error);
      }
    );
  }

  onBrandSelected(brandId: number) {
    this.brandIdSelected = brandId;
    this.getProducts();
  }

  onTypeSelected(typeId: number) {
    this.typeIdSelected = typeId;
    this.getProducts();
  }

  sortByPrice(val) {
    this.sortPrice = val;
    this.getProducts();
  }
}
