import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { AppComponent } from './app.component';
import { HomeComponent } from './components/home/home.component';
import { DataLoaderModule } from './data-loader/data-loader.module';
import { VisualizerModule } from './visualizer/visualizer.module';

@NgModule({
  declarations: [AppComponent, HomeComponent],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    DataLoaderModule,
    VisualizerModule,
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
