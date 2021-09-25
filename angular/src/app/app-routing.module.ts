import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { WeatherForecastComponent } from './weather-forecast/weather-forecast.component';
import { AutoLoginPartialRoutesGuard } from 'angular-auth-oidc-client';

const routes: Routes = [
  {path: 'home', component: HomeComponent},
  {path: 'weatherforecast', component: WeatherForecastComponent, canActivate: [AutoLoginPartialRoutesGuard]}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
