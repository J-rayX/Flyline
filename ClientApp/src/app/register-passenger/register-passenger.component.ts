import { Component, OnInit } from '@angular/core';
import { PassengerService } from './../api/services/passenger.service';
import { FormBuilder, Validators } from '@angular/forms';
import { AuthService } from '../auth/auth.service';
import { Router, ActivatedRoute } from '@angular/router';


@Component({
  selector: 'app-register-passenger',
  templateUrl: './register-passenger.component.html',
  styleUrls: ['./register-passenger.component.css']
})
export class RegisterPassengerComponent implements OnInit {

  constructor( //initiate the Angular & custom services needed for this component
    private passengerService: PassengerService,
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router,
    private activatedRoute: ActivatedRoute
  ) { }

  requestedUrl?: string = undefined

  // the following form is attached to the formGroup in the HTML template
  // it accepts and holds user input made on the HTML form 
  form = this.fb.group({
    email: ['', Validators.compose([Validators.required, Validators.minLength(3), Validators.maxLength(100)])],
    firstName: ['', Validators.compose([Validators.required, Validators.minLength(2), Validators.maxLength(35)])],
    lastName: ['', Validators.compose([Validators.required, Validators.minLength(2), Validators.maxLength(35)])],
    isFemale: [true, Validators.required]
  })

  ngOnInit(): void {
    this.activatedRoute.params.subscribe(p => this.requestedUrl = p['requestedUrl'])
  }

  checkPassenger(): void {
    const params = { email: this.form.get('email')?.value ?? '' }


    this.passengerService
      .findPassenger(params) 
      .subscribe(this.login)
  }

   //register() method is attached to the submit button on the HTML form
   //for user registration
  register() {

    if (this.form.invalid)
      return;

    console.log("Form Values:", this.form.value); //checks from browser console if form inputs reach this place
      

    this.passengerService.registerPassenger({ body: this.form.value }) // use passengerService to send form to the backend
      .subscribe(this.login, console.error);

  }

  private login = () => {
    this.authService.loginUser({ email: this.form.get('email')?.value ?? '' })
    this.router.navigate([this.requestedUrl ?? '/search-flights']) // handle redirect after login
  }

}

    //this.passengerService.registerPassenger({ body: this.form.value }) // use passengerService to send form to the backend
    //  .subscribe(_ => console.log("form posted to server")); // reports on successful post
