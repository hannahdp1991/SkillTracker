import { Component, Input, OnInit } from "@angular/core";
import { Observable } from "rxjs";
import { SkillProfile } from "src/models/skill-profile";
import { SkillTrackerService } from "src/services/skill-tracker.service";

@Component({
    templateUrl: "./skillprofile-viewer.component.html",
    selector: "skill-profile-viewer"
})
export class SkillProfileViewer {
    @Input() skillProfileList!: Array<SkillProfile>;
    displayedColumns = ["skillName", "expertiseLevel"];
}