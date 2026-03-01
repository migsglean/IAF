import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { EnvService } from '../env.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
    private baseUrl: string;
    private loggedIn = false;

    constructor(
        private http: HttpClient,
        private env: EnvService)
    {
        this.baseUrl = `${env.baseUrl}/auth`;
    }

    login(data: any): Observable<any> {
        return this.http.post(`${this.baseUrl}/login`, data);
    }

    signUp(data: any): Observable<any> {
        return this.http.post(`${this.baseUrl}/signup`, data);
    }

    forgotPassword(data: any): Observable<any> {
        return this.http.post(`${this.baseUrl}/forgot-password`, data);
    }

    setLoginSuccess() {
        this.loggedIn = true;
    }

    logout() {
        this.loggedIn = false;
    }

    isLoggedIn(): boolean {
        return this.loggedIn;
    }

}
