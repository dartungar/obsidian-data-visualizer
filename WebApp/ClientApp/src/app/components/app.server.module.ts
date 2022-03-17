import { NgModule } from '@angular/core';
import { ServerModule } from '@angular/platform-server';
import { ModuleMapLoaderModule } from '@nguniversal/module-map-ngfactory-loader';
import { AppComponent } from './app.component';
import { AppModule } from '../app.module';
import { DataLoaderComponent } from './data-loader/data-loader.component';
import { DashboardComponent } from './charts/dashboard/dashboard.component';
import { ChartComponent } from './charts/chart/chart.component';
import { AlertComponent } from './shared/alert/alert.component';
import { LayoutComponent } from './shared/layout/layout.component';

@NgModule({
    imports: [AppModule, ServerModule, ModuleMapLoaderModule],
    bootstrap: [AppComponent],
    declarations: [
      DataLoaderComponent,
      DashboardComponent,
      ChartComponent,
      AlertComponent,
      LayoutComponent
    ]
})
export class AppServerModule { }
