import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { QuickyTestComponent } from './quicky-test/quicky-test.component';
import { PromptModelComponent } from './sub-components/prompt-model/prompt-model.component';

@NgModule({
  declarations: [
    AppComponent,
    QuickyTestComponent,
    PromptModelComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: QuickyTestComponent, pathMatch: 'full' },
    ])
  ],
  providers: [],
  bootstrap: [AppComponent, QuickyTestComponent, PromptModelComponent]
})
export class AppModule { }
