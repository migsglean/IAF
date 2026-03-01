import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class EnvService {
  public baseUrl: string;
  public enableDebug: boolean;

  constructor() {
    const env = (window as any).__env;
    this.baseUrl = env.baseUrl || '';
    this.enableDebug = env.enableDebug || false;
  }
}
