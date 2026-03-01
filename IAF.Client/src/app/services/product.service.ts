import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { EnvService } from '../env.service';
import { ProduceDto, ProductDto } from '../domain/object-interface';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
    private baseUrl: string;

    constructor(
        private http: HttpClient,
        private env: EnvService)
    {
        this.baseUrl = `${env.baseUrl}/product`;
    }

    getProducts(): Observable<ProductDto> {
        return this.http.get<ProductDto>(`${this.baseUrl}/details`);
    }

    produceProduct(data: ProduceDto): Observable<any> {
        return this.http.post(`${this.baseUrl}/produce`, data);
    }
}
