import { Component, OnInit, ViewChild } from '@angular/core';
import { ProductDetails, ProductDto } from '../domain/object-interface';
import { ProductService } from '../services/product.service';
import { SwalHelper } from '../shared/swal-helper';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { UtilityService } from '../shared/utility.service';
import { ProductDetailsComponent } from '../product-details/product-details.component';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent implements OnInit {
    displayedColumns: string[] = ['productId', 'productDesc', 'forecastedProducedCount'];
    dataSource = new MatTableDataSource<ProductDetails>();

    @ViewChild(MatPaginator) paginator!: MatPaginator;
    @ViewChild(MatSort) sort!: MatSort;

    constructor(
        private productService: ProductService,
        private utilityService: UtilityService,
        private dialog: MatDialog
    ) { }

    ngOnInit() {
        this.getProducts();
        this.utilityService.getParts();
    }

    getProducts() {
        this.productService.getProducts().subscribe({
            next: (response: ProductDto) => {
                this.dataSource.data = response.productDetails;
                this.dataSource.paginator = this.paginator;
                this.dataSource.sort = this.sort;

                this.utilityService.productList = response.productDetails;
            },
            error: (err) => {
                if (err.error && err.error.responseDefaultDto) {
                    SwalHelper.error(err.error.responseDefaultDto.message);
                } else {
                    SwalHelper.error('Unexpected error occurred while fetching products.');
                }
            }
        });
    }

    applyFilter(event: Event) {
        const filterValue = (event.target as HTMLInputElement).value;
        this.dataSource.filter = filterValue.trim().toLowerCase();
    }

    onRowClick(row: ProductDetails) {
        const dialogRef = this.dialog.open(ProductDetailsComponent, {
            width: '800px',
            data: row
        });

        dialogRef.afterClosed().subscribe(() => {
            this.getProducts();
        });
    }
}

