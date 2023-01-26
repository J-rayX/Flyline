import { Component } from '@angular/core';
import { PassengerService } from './../api/services/passenger.service';
import { FormBuilder } from '@angular/forms';

@Component({
  selector: 'app-register-passenger',
  templateUrl: './register-passenger.component.html',
  styleUrls: ['./register-passenger.component.css']
})
export class RegisterPassengerComponent {

  constructor( //initiate the Angular & custom services needed for this component
    private passengerService: PassengerService,
    private fb: FormBuilder
  ) { }

  // the following form is attached to the formGroup in the HTML template
  // it accepts and holds user input made on the HTML form 
  form = this.fb.group({
    email: [''],
    firstName: [''],
    lastName: [''],
    isFemale: [true] 
  })

  // register() method is attached to the submit button on the HTML form
  // for user registration
  register() {
    console.log("Form Values:", this.form.value); //checks from browser console if form inputs reach this place
    this.passengerService.registerPassenger({ body: this.form.value }) // use passengerService to send form to the backend
      .subscribe(_ => console.log("form posted to server")); // reports on successful post
  }
}
