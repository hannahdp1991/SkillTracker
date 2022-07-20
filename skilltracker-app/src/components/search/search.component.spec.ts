import { FormBuilder } from "@angular/forms";
import { of } from "rxjs";
import { AssociateDetails } from "src/models/associate-details";
import { Skill } from "src/models/skill";
import { SkillProfile } from "src/models/skill-profile";
import { SkillTrackerService } from "src/services/skill-tracker.service";
import { SearchComponent } from "./search.component";

describe("Search component", () => {
    var component: SearchComponent;
    var mockSkillTrackerService: SkillTrackerService;

    const mockFormbuilder = new FormBuilder();

    const skillProfile1 = new SkillProfile();
    const skillProfile2 = new SkillProfile();
    const skillProfile3 = new SkillProfile();

    const skill1 = new Skill();
    skill1.expertiseLevel = 12;
    skill1.skillName = "GIT";

    const skill2 = new Skill();
    skill1.expertiseLevel = 12;
    skill1.skillName = "ANGULAR";

    const skill3 = new Skill();
    skill1.expertiseLevel = 12;
    skill1.skillName = "CSS";

    const skill4 = new Skill();
    skill1.expertiseLevel = 12;
    skill1.skillName = "JENKINS";

    const associate1 = new AssociateDetails();
    associate1.associateId = "123";
    associate1.name = "associate1";
    associate1.email = "associate1@test.com"
    associate1.mobile = "1234567899"

    const associate2 = new AssociateDetails();
    associate2.associateId = "10001";
    associate2.name = "associate2";
    associate2.email = "associate2@test.com"
    associate2.mobile = "9994735761"

    const associate3 = new AssociateDetails();
    associate1.associateId = "20001";
    associate1.name = "associate3";
    associate1.email = "associate3@test.com"
    associate1.mobile = "7891261221"

    skillProfile1.associateInfo = associate1;
    skillProfile1.skillInfo = [skill1, skill2];

    skillProfile2.associateInfo = associate2;
    skillProfile2.skillInfo = [skill1, skill2, skill3];

    skillProfile3.associateInfo = associate3;
    skillProfile3.skillInfo = [skill3, skill4];

    const skillProfileList = [skillProfile1, skillProfile2, skillProfile3];

    beforeEach(() => {
        mockSkillTrackerService = jasmine.createSpyObj(SkillTrackerService.name, ["getSkillsByCriteria"]);
        (mockSkillTrackerService.getSkillsByCriteria as jasmine.Spy).and.callFake((arg1, arg2) => {
            switch (arg1) {
                case "associateid":
                    return of(skillProfileList.filter(skillProfile => {
                        skillProfile.associateInfo.associateId === arg2
                    }));
                case "associatename":
                    return of(skillProfileList.filter(skillProfile => {
                        skillProfile.associateInfo.name === arg2
                    }));
                case "skillname":
                    return of(skillProfileList.filter(skillProfile => {
                        skillProfile.skillInfo.some(skill => skill.skillName === arg2)
                    }));
                default:
                    return of(null);
            }
        });
        component = new SearchComponent(mockFormbuilder, mockSkillTrackerService);
    });

    describe("ngOnInit", () => {
        it("should intitalize the search form", () => {
            component.ngOnInit();
            expect(component.searchForm).not.toBeNull();
            expect(component.searchForm.get("criteriaType")?.value).toBe("");
            expect(component.searchForm.get("criteriaValue")?.value).toBe("");
        });
    });

    describe("onSubmit", () => {
        it("should return the skill list based on search criteria", () => {
            component.ngOnInit();
            component.searchForm.setValue({
                criteriaType: "associateid",
                criteriaValue: "123"
            });

            component.onSubmit();

            component.skillProfileList$.subscribe(response => {
                expect(response).not.toBeNull();
                expect(response[0].associateInfo).toBe(associate1);
            });
        });

        it("should return null if no skill profiles match criteria", () => {
            component.ngOnInit();
            component.searchForm.setValue({
                criteriaType: "associateid",
                criteriaValue: "444"
            });

            component.onSubmit();

            component.skillProfileList$.subscribe(response => {
                expect(response.length).toEqual(0);
            });
        });
    });
});