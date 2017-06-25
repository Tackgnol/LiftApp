class Community():
    def __init__(self, id):
        self.Id = id
        self.Neighbors = []
        self.Candidates = []
        self.Owner = None


class Candidate():
    def __init__(self, id, startingCommunity):   
        self.Id = id
        self.StartingCommunity = startingCommunity

class Application():
    def __init__(self):
        self.NoOfCommunities = None
        self.CommunityList = []
        self.NoOfCandidates = None
        self.CandidateList = []
        self.RelationList = []

    def ReadInput(self, input):
        lineList = input.split('\n')

        self.NoOfCommunities = int(lineList[0])
        relationsStop = lineList.index("-1 -1")
        for i in range(1,relationsStop):
            self.RelationList.append((int(lineList[i][0]), int(lineList[i][2])))
        resume = relationsStop+1
        self.NoOfCandidates = int(lineList[resume])
        resume+=1
        for i in range(resume, resume+self.NoOfCandidates):
            self.CandidateList.append(Candidate(int(i-resume),int(lineList[i])))
    def BuildCommunities(self):
        for i in range(0, self.NoOfCommunities):
            tempCommunity = Community(i)
            for relation in self.RelationList:
                if relation[0] == i:
                    tempCommunity.Neighbors.append(relation[1])
                if relation[1] == i:
                    tempCommunity.Neighbors.append(relation[0])
            self.CommunityList.append(tempCommunity)
    def InitialSetUp(self):
        for candidate in self.CandidateList:
            self.CommunityList[int(candidate.StartingCommunity)].Owner = candidate.Id


App = Application()

input = "6\n1 0\n2 1\n3 0\n3 1\n3 2\n4 2\n4 3\n5 0\n5 2\n-1 -1\n3\n0\n1\n5\n"

App.ReadInput(input)
App.BuildCommunities()
App.InitialSetUp()
print('Input Read')