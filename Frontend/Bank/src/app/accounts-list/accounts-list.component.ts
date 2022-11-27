import { Component } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';

declare var window: any;

@Component({
  selector: 'app-accounts-list',
  templateUrl: './accounts-list.component.html',
  styleUrls: ['./accounts-list.component.scss']
})
export class AccountsListComponent {

  accounts = new Array<any>();
  formModal: any;
  formModal2: any;

  constructor(private http: HttpClient) { }
  cekilecekMiktar: number = 0;
  yatirilacakMiktar: number = 0;
  id: number = 0;
  errorMessage: string = '';

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json; charset=utf-8'
    })
  };

  ngOnInit() {

    this.getAccount()
    this.formModal = new window.bootstrap.Modal(
      document.getElementById('paraYatır')
    );
    this.formModal2 = new window.bootstrap.Modal(
      document.getElementById('paraCek')
    );

  }

  getAccount() {
    this.http.get<any>('https://localhost:7123/account').subscribe(response => {
      this.accounts = response;
      console.log(this.accounts)
    })
  }
  openFormModal(id: number) {
    this.errorMessage = ''
    this.yatirilacakMiktar = 0;
    this.id = id;
    this.formModal.show();
  }
  yatir() {
    console.log('yatıra basıldı')
    const body = JSON.stringify({
      id: this.id,
      balance: this.yatirilacakMiktar
    });
    console.log(body)
    this.http.post('https://localhost:7123/deposit', body, this.httpOptions).subscribe(response => {
      console.log(response)
      this.formModal.hide();
      this.getAccount();
    }, (error) => {
      console.error(error);
      this.errorMessage = error.error.message;
    })
    this.errorMessage = ''
    this.yatirilacakMiktar = 0;
  }
  openFormModal2(id: number) {
    this.errorMessage = ''
    this.cekilecekMiktar = 0;
    this.id = id;
    this.formModal2.show();
  }
  cek() {
    console.log(this.cekilecekMiktar)
    var httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      }),
    };
    const body = JSON.stringify({
      id: this.id,
      balance: this.cekilecekMiktar
    });
    console.log(body)
    this.http.post<any>('https://localhost:7123/withdraw', body, this.httpOptions).subscribe(response => {
      console.log(response)
      this.formModal2.hide();
      this.getAccount();
     
    }, (error) => {
      console.error(error);
      this.errorMessage = error.error.message;
      
    })
    
    
  }
}

