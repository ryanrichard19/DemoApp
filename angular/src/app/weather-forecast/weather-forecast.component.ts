import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { WeatherForecast } from '../weather-forecast';
import { WeatherForecastService } from '../weather-forecast.service';

@Component({
  selector: 'app-weather-forecast',
  templateUrl: './weather-forecast.component.html',
  styleUrls: ['./weather-forecast.component.scss']
})
export class WeatherForecastComponent implements OnInit {
  weatherForecast$: Observable<WeatherForecast[]> | undefined;

  constructor(private weatherForecastService: WeatherForecastService) { }

  ngOnInit(): void {
    this.weatherForecast$ = this.weatherForecastService.get();
  }



}
