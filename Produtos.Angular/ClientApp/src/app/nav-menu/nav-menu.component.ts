import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UsuarioServico } from '../servicos/usuario/usuario.servico';


@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit {

  isExpanded = false;
 

  get usuario() {
    return this.usuarioServico.usuario;
  }


  ngOnInit(): void {
   
  }

  constructor(private router: Router, private usuarioServico: UsuarioServico) {

  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  // Verificae se o usuario está autenticado
  public usuarioAutenticado(): boolean {
    return this.usuarioServico.usuarioAutenticado();
  }


  sair() {    
    this.usuarioServico.limparSessao();
    this.router.navigate(['/entrar']);
  }

  
}
