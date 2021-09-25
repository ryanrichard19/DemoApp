import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { WeatherForecast } from './weather-forecast';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class WeatherForecastService {

  constructor(private http: HttpClient) { }

  get(): Observable<WeatherForecast[]> {
    return this.http.get<WeatherForecast[]>('https://localhost:7179/WeatherForecast');
  }
}
