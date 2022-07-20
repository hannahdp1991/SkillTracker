import { Injectable } from "@angular/core";
import { catchError, map, Observable, of, tap } from "rxjs";
import { SkillProfile } from "src/models/skill-profile";
import { HttpClient } from "@angular/common/http";

@Injectable()
export class SkillTrackerService {

    constructor(private readonly httpClient: HttpClient) {
    }

    public getSkills(): Observable<Array<SkillProfile>> {
        return this.httpClient.get<Array<SkillProfile>>("http://localhost:9000/adminservice").pipe(
            map(response => response as Array<SkillProfile>),
            catchError(_ => of([])));
    }

    public getSkillsByCriteria(criteriaType: string, criterialValue: string): Observable<Array<SkillProfile>> {
        return this.httpClient.get<Array<SkillProfile>>("http://localhost:9000/adminservice/" + criteriaType + "/" + criterialValue).pipe(
            map(response => response as Array<SkillProfile>),
            catchError(_ => of([])));
    }
}