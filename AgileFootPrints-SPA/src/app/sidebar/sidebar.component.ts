import { Component, OnInit } from '@angular/core';
import { SidebarService } from '../_services/sidebar.service';
import {
  trigger,
  state,
  style,
  transition,
  animate
} from '@angular/animations';
import { ProjectService } from '../_services/project.service';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';
import { EpicService } from '../_services/epic.service';
import { Router, NavigationEnd } from '@angular/router';
import { SprintService } from '../_services/sprint.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { EpicComponent } from '../Epic/Epic.component';
import { NotificationService } from '../_services/notification.service';
import { ContributorService } from '../_services/contributor.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.scss'],
  animations: [
    trigger('slide', [
      state('up', style({ height: 0 })),
      state('down', style({ height: '*' })),
      transition('up <=> down', animate(200))
    ])
  ]
})
export class SidebarComponent implements OnInit {
  userId = this.authService.decodedToken.nameid;
  navigationSubscription;
  userProjects = [];
  projectId: string;
  menus = [];
  routes = [];
  notifications: any = [];
  mails: any = [];
  space = ' ';
  mailsUnReadCount = 0;
  notificationsUnReadCount = 0;
  matSpinner = false;
  matSprinnerColor = 'warn';

  constructor(
    private notificationService: NotificationService,
    public authService: AuthService,
    public sidebarservice: SidebarService,
    private contributorService: ContributorService,
    private alertify: AlertifyService
  ) {
    this.routes = ['/project', '', '/usertasks'];
    this.menus = sidebarservice.getMenuList();
  }

  ngOnInit() {
    this.matSpinner = false;
    this.matSprinnerColor = 'warn';
    this.notificationsUnReadCount = 0;
    this.mailsUnReadCount = 0;
    this.getNotifications();
  }
  toggleNotificationSpinner() {
    this.notificationsUnReadCount = 0;
    this.matSpinner = true;
    this.getNotifications();
  }
  toggleMailsSpinner() {
    this.mailsUnReadCount = 0;
    this.matSpinner = true;
    this.getNotifications();
  }
  getSideBarState() {
    return this.sidebarservice.getSidebarState();
  }

  toggle(currentMenu) {
    if (currentMenu.type === 'dropdown') {
      this.menus.forEach(element => {
        if (element === currentMenu) {
          currentMenu.active = !currentMenu.active;
        } else {
          element.active = false;
        }
      });
    }
  }

  getState(currentMenu) {
    if (currentMenu.active) {
      return 'down';
    } else {
      return 'up';
    }
  }

  hasBackgroundImage() {
    return this.sidebarservice.hasBackgroundImage;
  }

  getNotifications() {
    this.notificationService.getNotifications().subscribe(
      data => {
        console.log(data);
        data.forEach(element => {
          if (element.isMail === true) {
            this.mails.push(element);
          } else {
            this.notifications.push(element);
          }
        });

        this.notifications.forEach(element => {
          if (element.isRead === false && element.isNotification === true) {
            this.notificationsUnReadCount += 1;
          }
          setInterval(() => {
            this.matSpinner = false;
          }, 2000);
        });
        this.mails.forEach(element => {
          if (element.isRead === false && element.isMail === true) {
            this.mailsUnReadCount += 1;
          }
          setInterval(() => {
            this.matSpinner = false;
          }, 2000);
        });
        console.log(this.notifications);
      },
      error => {
        this.alertify.error(error.message);
      }
    );
    console.log(this.notifications);
  }

  accept(notificationModel: any) {
    this.contributorService
      .acceptContributorInviatation(notificationModel)
      .subscribe(() => {
        this.getNotifications();
        Swal.fire({
          title: 'Great!',
          text: 'You are no contributing the project',
          type: 'success',
          confirmButtonText: 'OK'
        });
      });
  }
}
