import { Component, inject } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';
import { filter } from 'rxjs';

@Component({
  selector: 'app-add-hr-final',
  templateUrl: './add-hr-final.component.html',
  styleUrl: './add-hr-final.component.css'
})
export class AddHrFinalComponent {
  router : Router = inject(Router);

  ngOnInit() {
    this.router.events.pipe(
      filter(event => event instanceof NavigationEnd)
    ).subscribe((event: NavigationEnd) => {
      window.scrollTo(0, 0);
    });
    document.body.scrollTop = 0;
  }
}
