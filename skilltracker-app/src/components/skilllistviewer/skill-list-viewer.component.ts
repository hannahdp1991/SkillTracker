import { Component, OnInit } from "@angular/core";
import { Observable } from "rxjs";
import { SkillProfile } from "src/models/skill-profile";
import { SkillTrackerService } from "src/services/skill-tracker.service";

@Component({ templateUrl: "./skill-list-viewer.component.html" })
export class SkillProfileListViewer implements OnInit {
    skillProfileList$!: Observable<Array<SkillProfile>>;
    displayedColumns = ["skillName", "expertiseLevel"];

    constructor(private readonly skillService: SkillTrackerService) { }

    public ngOnInit(): void {
        this.skillProfileList$ = this.skillService.getSkills();
    }
}