import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable, BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class EpicService {
  private IdSource = new BehaviorSubject('');
  currentId = this.IdSource.asObservable();

  baseUrl = environment.apiUrl + 'epic/';
  storyBaseUrl = environment.apiUrl + 'story/';

  constructor(private http: HttpClient) {}
  changeId(id: string) {
    this.IdSource.next(id);
  }
  getProjectEpics(id: string): Observable<any> {
    return this.http.get<any>(this.baseUrl + 'getEpics/' + id);
  }

  getEpicStories(id: string): Observable<any> {
    return this.http.get<any>(this.baseUrl + 'getEpicStories/' + id);
  }
  getEpic(id: string): Observable<any> {
    return this.http.get<any>(this.baseUrl + 'getEpic/' + id);
  }
}
