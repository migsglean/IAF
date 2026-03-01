import { ChangeDetectorRef, Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { PartsDetails, ProduceDto, ProductDetails } from '../domain/object-interface';
import { SwalHelper } from 'src/app/shared/swal-helper';
import { UtilityService } from '../shared/utility.service';
import { ProductService } from '../services/product.service';

@Component({
    selector: 'app-product-details',
    templateUrl: './product-details.component.html',
    styleUrls: ['./product-details.component.css']
})
export class ProductDetailsComponent implements OnInit {
    produceQuantity: number = 0;
    partsList: PartsDetails[] = [];
    produceRequest: ProduceDto = {
        userName: '',
        productId: '',
        partsId: [],
        quantity: 0
    }

    constructor(
        @Inject(MAT_DIALOG_DATA) public data: ProductDetails,
        private utilityService: UtilityService,
        private productService: ProductService,
        private cdRef: ChangeDetectorRef
    ) { }

    ngOnInit() {
        this.utilityService.partsList$.subscribe(parts => {
            this.partsList = parts.filter(p => p.productId === this.data.productId);
            this.cdRef.detectChanges(); 
        });

        this.utilityService.getParts();
    }

    produce() {
        const qtyToProduce = this.produceQuantity;
        const messages: string[] = [];

        if (qtyToProduce > this.data.forecastedProducedCount) {
            messages.push(`Cannot produce more than forecasted (${this.data.forecastedProducedCount}).`);
        }

        const missingParts: string[] = [];
        this.partsList.forEach(part => {
            if (part.quantity < qtyToProduce) {
                missingParts.push(`${part.partsDesc} (Available: ${part.quantity})`);
            }
        });

        if (missingParts.length > 0) {
            messages.push(`Insufficient parts:<br>${missingParts.join('<br>')}`);
        }

        if (messages.length > 0) {
            SwalHelper.error(messages.join('<br>'));
            return; 
        }

        this.partsList.forEach(part => {
            if (part.partsId != null) {
                this.produceRequest.partsId.push(part.partsId);
            }
        });

        this.produceRequest.userName = this.utilityService.userName;
        this.produceRequest.quantity = qtyToProduce;
        this.produceRequest.productId = this.data.productId;

        this.productService.produceProduct(this.produceRequest).subscribe({
            next: (response) => {
                this.utilityService.getParts();
                SwalHelper.success(response.message);
                this.cdRef.detectChanges();
            },
            error: (err) => {
                const message = err.error?.message || 'Unexpected error occurred while producing product.';
                SwalHelper.error(message);
                this.cdRef.detectChanges(); 
            }
        });

    }
}