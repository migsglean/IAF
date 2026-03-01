import { Injectable } from '@angular/core';
import { PartService } from '../services/part.service';
import { PartsDetails, PartsDto, ProductDetails } from '../domain/object-interface';
import { SwalHelper } from './swal-helper';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UtilityService {
    userName: string = '';
    productList: ProductDetails[] = [];
    private partsListSubject = new BehaviorSubject<PartsDetails[]>([]);
    partsList$ = this.partsListSubject.asObservable();

    constructor(private partsService: PartService) { }

    getProductSummaries(): { productId: string; productDesc: string }[] {
        return this.productList.map(p => ({
            productId: p.productId,
            productDesc: p.productDesc
        }));
    }
 
    clearProducts(): void {
        this.productList = [];
    }

    clearParts(): void {
        this.partsListSubject.next([]);
    }

    getParts() {
        this.partsService.getParts().subscribe({
            next: (response: PartsDto) => {
                this.partsListSubject.next(response.partsDetails);
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
}
