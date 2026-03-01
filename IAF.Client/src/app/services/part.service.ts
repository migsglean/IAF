import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { EnvService } from '../env.service';
import { PartsDto, PartsDetails, ResponseDefaultDto } from '../domain/object-interface';

@Injectable({
  providedIn: 'root'
})
export class PartService {
    private baseUrl: string;

    constructor(
        private http: HttpClient,
        private env: EnvService) {
        this.baseUrl = `${env.baseUrl}/parts`;
    }

    getParts(): Observable<PartsDto> {
        return this.http.get<PartsDto>(`${this.baseUrl}/details`);
    }

    addPart(part: PartsDetails): Observable<ResponseDefaultDto> {
        return this.http.post<ResponseDefaultDto>(`${this.baseUrl}/insert`, part);
    }

    updatePart(part: PartsDetails): Observable<ResponseDefaultDto> {
        return this.http.put<ResponseDefaultDto>(`${this.baseUrl}/update`, part);
    }

    deletePart(partsId: string): Observable<ResponseDefaultDto> {
        return this.http.delete<ResponseDefaultDto>(`${this.baseUrl}/delete/${partsId}`);
    }
}
