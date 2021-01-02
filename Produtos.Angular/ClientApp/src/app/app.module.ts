import { BrowserModule } from '@angular/platform-browser';
import { NgModule, LOCALE_ID } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { TruncateModule } from 'ng2-truncate';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';

import { registerLocaleData } from '@angular/common';
import localePt from '@angular/common/locales/pt';
registerLocaleData(localePt, 'pt');

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
// Importar componentes criados aqui
import { ProdutoComponent } from './produto/produto.component';
import { LoginComponent } from './usuario/login/login.component';
import { GuardaRotas } from './autorizacao/guarda.rotas';
import { UsuarioServico } from './servicos/usuario/usuario.servico';
import { CadastroUsuarioComponent } from './usuario/cadastro/cadastro.usuario.component';
import { ProdutoServico } from './servicos/produto/produto.servico';
import { PesquisaProdutoComponent } from './produto/pesquisa/pesquisa.produto.component';
import { LojaPesquisaComponent } from './loja/pesquisa/loja.pesquisa.component';
import { LojaProdutoComponent } from './loja/produto/loja.produto.component';

import { PedidoServico } from './servicos/pedido/pedido.servico';



@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
  
    // declaracao de componentes
    ProdutoComponent,
    LoginComponent,
    CadastroUsuarioComponent,
    PesquisaProdutoComponent,
    LojaPesquisaComponent,
    LojaProdutoComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    TruncateModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot({
      timeOut: 10000,
      positionClass: 'toast-top-right',
      closeButton: true
    }),
    RouterModule.forRoot([
      { path: '', redirectTo:'entrar', pathMatch: 'full' },
      { path: 'entrar', component: LoginComponent },
  
      { path: 'produto', component: ProdutoComponent, canActivate: [GuardaRotas] }, // mapeamento das rotas
      { path: 'pesquisar-produto', component: PesquisaProdutoComponent, canActivate: [GuardaRotas] },
      { path: 'novo-usuario', component: CadastroUsuarioComponent },
      { path: 'loja-produto', component: LojaProdutoComponent }
    ])
  ],
  providers: [{ provide: LOCALE_ID, useValue: 'pt' },
    UsuarioServico,
    ProdutoServico,
    PedidoServico], // Configurar os serviços
  bootstrap: [AppComponent]
})
export class AppModule { }
