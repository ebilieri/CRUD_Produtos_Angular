import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Usuario } from '../../modelo/usuario';
//import { debug } from 'console';
//import { debug } from 'console';

@Injectable({
  providedIn: 'root'
})

export class UsuarioServico {

  private baseURL: string;
  private _usuario: Usuario;

  get usuario(): Usuario {
    debugger;
    let usuario_json = sessionStorage.getItem("usuario-autenticado");
    this._usuario = JSON.parse(usuario_json);
    return this._usuario;
  }

  set usuario(usuario: Usuario) {
    debugger;
    sessionStorage.setItem("usuario-autenticado", JSON.stringify(usuario));
    this._usuario = usuario;
  }

  get headers(): HttpHeaders {
    return new HttpHeaders().set('content-type', 'application/json');
  }

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseURL = baseUrl;
  }

  public usuarioAutenticado(): boolean {
   
    return this._usuario != null && this._usuario.success == true;
  }

  public limparSessao(): void {
    sessionStorage.setItem("usuario-autenticado", "");
    this._usuario = null;
  }

  public verificarUsuario(usuario: Usuario): Observable<Usuario> {
    //debugger;
    // url raiz + endereço do serviço a ser chamado ex.: https://www.groceryshop.com/
    return this.http.post<Usuario>(this.baseURL + 'api/usuario', JSON.stringify(usuario), { headers: this.headers });
  }

  /*
   * Cadastrar novo usuario
   */
  public cadastrarUsuario(usuario: Usuario): Observable<Usuario> {

    return this.http.post<Usuario>(this.baseURL + 'api/usuario', JSON.stringify(usuario), { headers: this.headers });
  }
}
