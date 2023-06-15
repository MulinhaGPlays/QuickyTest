import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { QuickyTestComponent } from './quicky-test/quicky-test.component';
import { PromptModelComponent } from './sub-components/prompt-model/prompt-model.component';
import { ProofLayoutComponent } from './sub-components/proof-layout/proof-layout.component';
import { VisualProofComponent } from './sub-components/visual-proof/visual-proof.component';

@NgModule({
  declarations: [
    AppComponent,
    QuickyTestComponent,
    PromptModelComponent,
    ProofLayoutComponent,
    VisualProofComponent,
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
  bootstrap: [AppComponent, QuickyTestComponent, PromptModelComponent, ProofLayoutComponent, VisualProofComponent]
})
export class AppModule { }
