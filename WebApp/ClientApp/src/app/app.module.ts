import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { AppComponent } from './app.component';
import { HomeComponent } from './general/home/home.component';
import { DataLoaderModule } from './data-loader/data-loader.module';
import { VisualizerModule } from './visualizer/visualizer.module';
import { NavComponent } from './general/nav/nav.component';
import { RouterModule } from '@angular/router';
import { DataLoaderComponent } from './data-loader/data-loader/data-loader.component';
import { DashboardComponent } from './visualizer/dashboard/dashboard.component';
import { LayoutComponent } from './general/layout/layout.component';
import { DataExplorerComponent } from './visualizer/data-explorer/data-explorer.component';
import { NotificationsModule } from './notifications/notifications.module';

@NgModule({
  declarations: [AppComponent, HomeComponent, NavComponent, LayoutComponent],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    RouterModule.forRoot([
      { path: '', component: HomeComponent },
      { path: 'load', component: DataLoaderComponent },
      { path: 'dashboard', component: DashboardComponent },
      { path: 'explore', component: DataExplorerComponent },
    ]),
    HttpClientModule,
    FormsModule,
    DataLoaderModule,
    VisualizerModule,
    NotificationsModule,
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
